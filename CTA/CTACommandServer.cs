using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CaveSharp.MetaAI;

namespace CaveSharp.CTA
{
    /// <summary>
    /// Receives CTA commands from external tools or dashboards.
    /// Overseer.cs is the authority that executes the commands.
    /// </summary>
    public class CTACommandServer
    {
        private readonly Overseer _overseer;
        private readonly HttpListener _listener;

        public CTACommandServer(Overseer overseer)
        {
            _overseer = overseer;

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8088/cta/");
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine("[CTACommandServer] Listening on ws://localhost:8088/cta");

            while (true)
            {
                var ctx = await _listener.GetContextAsync();

                if (ctx.Request.IsWebSocketRequest)
                {
                    _ = HandleWebSocket(ctx);
                }
                else
                {
                    ctx.Response.StatusCode = 400;
                    ctx.Response.Close();
                }
            }
        }

        private async Task HandleWebSocket(HttpListenerContext ctx)
        {
            WebSocketContext wsContext = await ctx.AcceptWebSocketAsync(null);
            WebSocket socket = wsContext.WebSocket;

            Console.WriteLine("[CTACommandServer] WebSocket client connected.");

            var buffer = new byte[2048];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    break;
                }

                string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                ProcessCommand(msg);
            }
        }

        private void ProcessCommand(string msg)
        {
            Console.WriteLine($"[CTACommandServer] Received: {msg}");

            string[] parts = msg.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return;

            string cmd = parts[0].ToLower();

            switch (cmd)
            {
                case "increase":
                    if (parts.Length > 1 && float.TryParse(parts[1], out float inc))
                        _overseer.IncreaseCTA(inc);
                    break;

                case "cooldown":
                    if (parts.Length > 1 && float.TryParse(parts[1], out float cd))
                        _overseer.CooldownCTA(cd);
                    break;

                case "directive":
                    string directive = msg.Substring("directive".Length).Trim();
                    _overseer.SetCTADirective(directive);
                    break;

                default:
                    Console.WriteLine($"[CTACommandServer] Unknown command: {cmd}");
                    break;
            }
        }
    }
}

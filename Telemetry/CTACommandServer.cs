using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CaveSharp.MetaAI;

namespace CaveSharp.Telemetry
{
    /// <summary>
    /// Receives CTA commands from RB-App cockpit and routes them to Overseer.
    /// </summary>
    public class CTACommandServer
    {
        private readonly Overseer _overseer;
        private readonly HttpListener _listener;

        public CTACommandServer(Overseer overseer, string url = "http://localhost:8282/cta/")
        {
            _overseer = overseer;

            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine("[CTACommandServer] Listening for CTA commands...");

            while (true)
            {
                var ctx = await _listener.GetContextAsync();

                if (ctx.Request.IsWebSocketRequest)
                {
                    var wsContext = await ctx.AcceptWebSocketAsync(null);
                    _ = Handle(wsContext.WebSocket);
                }
                else
                {
                    ctx.Response.StatusCode = 400;
                    ctx.Response.Close();
                }
            }
        }

        private async Task Handle(WebSocket socket)
        {
            var buffer = new byte[1024];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    break;
                }

                string cmd = Encoding.UTF8.GetString(buffer, 0, result.Count);
                ProcessCommand(cmd);
            }
        }

        private void ProcessCommand(string cmd)
        {
            cmd = cmd.Trim().ToLower();

            if (cmd == "increase")
            {
                _overseer.IncreaseCTA(10);
                Console.WriteLine("[CTACommandServer] CTA increased.");
            }
            else if (cmd == "cooldown")
            {
                _overseer.CooldownCTA(10);
                Console.WriteLine("[CTACommandServer] CTA cooled.");
            }
            else
            {
                _overseer.SetCTADirective(cmd);
                Console.WriteLine($"[CTACommandServer] Directive set: {cmd}");
            }
        }
    }
}

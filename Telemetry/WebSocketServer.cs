using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaveSharp.Telemetry
{
    /// <summary>
    /// Broadcasts telemetry from Overseer to RB-App cockpit.
    /// Supports multiple WebSocket clients.
    /// </summary>
    public class WebSocketServer
    {
        private readonly HttpListener _listener;
        private readonly List<WebSocket> _clients = new();

        public WebSocketServer(string url = "http://localhost:8181/telemetry/")
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine("[WebSocketServer] Telemetry server started.");

            while (true)
            {
                var ctx = await _listener.GetContextAsync();

                if (ctx.Request.IsWebSocketRequest)
                {
                    var wsContext = await ctx.AcceptWebSocketAsync(null);
                    var socket = wsContext.WebSocket;

                    _clients.Add(socket);
                    Console.WriteLine("[WebSocketServer] Client connected.");

                    _ = Listen(socket);
                }
                else
                {
                    ctx.Response.StatusCode = 400;
                    ctx.Response.Close();
                }
            }
        }

        private async Task Listen(WebSocket socket)
        {
            var buffer = new byte[1024];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    _clients.Remove(socket);
                    Console.WriteLine("[WebSocketServer] Client disconnected.");
                    break;
                }
            }
        }

        /// <summary>
        /// Overseer calls this to broadcast telemetry JSON.
        /// </summary>
        public async Task SendToAll(string json)
        {
            var bytes = Encoding.UTF8.GetBytes(json);

            foreach (var socket in _clients.ToArray())
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    _clients.Remove(socket);
                }
            }
        }
    }
}

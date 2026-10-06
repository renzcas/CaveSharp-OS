using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RB_App
{
    public class TelemetryClient
    {
        private readonly string _url;
        private readonly Dashboard _dashboard;

        public TelemetryClient(string url, Dashboard dashboard)
        {
            _url = url;
            _dashboard = dashboard;
        }

        public async Task StartAsync(CancellationToken token)
        {
            using var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri(_url), token);

            var buffer = new byte[4096];

            while (!token.IsCancellationRequested)
            {
                var result = await ws.ReceiveAsync(buffer, token);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                ProcessMessage(msg);
            }
        }

        private void ProcessMessage(string msg)
        {
            if (msg.StartsWith("CTA:"))
            {
                _dashboard.UpdateCTA(msg.Substring(4));
            }
            else if (msg.StartsWith("CREATURES:"))
            {
                int count = int.Parse(msg.Substring(10));
                _dashboard.UpdateCreatureCount(count);
            }
            else if (msg.StartsWith("HEATMAP:"))
            {
                string raw = msg.Substring(8);
                string[] rows = raw.Split(';');

                int[,] map = new int[rows.Length, rows[0].Split(',').Length];

                for (int y = 0; y < rows.Length; y++)
                {
                    var cols = rows[y].Split(',');
                    for (int x = 0; x < cols.Length; x++)
                    {
                        map[y, x] = int.Parse(cols[x]);
                    }
                }

                _dashboard.UpdateHeatmap(map);
            }
        }
    }
}

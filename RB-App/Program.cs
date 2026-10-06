using System;
using System.Threading;
using System.Threading.Tasks;

namespace RB_App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dashboard = new Dashboard();
            var cts = new CancellationTokenSource();

            var telemetryClient = new TelemetryClient("ws://localhost:8765", dashboard);
            var telemetryTask = telemetryClient.StartAsync(cts.Token);

            Console.WriteLine("RB-App Cockpit Running. Press Ctrl+C to exit.");

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            while (!cts.IsCancellationRequested)
            {
                Console.Clear();
                dashboard.Render();
                await Task.Delay(500);
            }
        }
    }
}

using System;
using CaveSharpOS.BigAnimal;
using CaveSharpOS.Time.Demo;

namespace CaveSharp.Core
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "--legacy")
            {
                Console.WriteLine("[CaveSharp-OS] Booting legacy engine...");
                new UpdateLoop().Start();
                return 0;
            }

            var runBigAnimal = Array.IndexOf(args, "--big-animal") >= 0;
            var tickArgs = runBigAnimal
                ? Array.FindAll(args, argument => argument != "--big-animal")
                : args;
            var ticks = 200;
            if (tickArgs.Length > 0)
            {
                if (tickArgs.Length != 2
                    || tickArgs[0] != "--ticks"
                    || !int.TryParse(tickArgs[1], out ticks)
                    || ticks < 0)
                {
                    Console.Error.WriteLine("Usage: dotnet run -- [--ticks <non-negative-count>] [--big-animal] | --legacy");
                    return 2;
                }
            }

            if (runBigAnimal)
            {
                Console.WriteLine($"[CaveSharp-OS] Running BIG_ANIMAL bridge demo for {ticks} ticks.");
                BigAnimalSimulationDriver.RunDemo(ticks, TimeSpan.FromMilliseconds(50));
                Console.WriteLine("[CaveSharp-OS] Bridge demo complete. Logs: logs/world_state.jsonl");
                return 0;
            }

            Console.WriteLine($"[CaveSharp-OS] Running TimeEngine simulation for {ticks} ticks.");
            DemoBootstrap.Run(ticks, TimeSpan.FromMilliseconds(50));
            Console.WriteLine("[CaveSharp-OS] Simulation complete. Logs: logs/world_state.jsonl");
            return 0;
        }
    }
}

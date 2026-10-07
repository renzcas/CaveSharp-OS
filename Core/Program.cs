using System;

namespace CaveSharp.Core
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("[CaveSharp-OS] Booting engine...");

            var loop = new UpdateLoop();
            loop.Start();

            Console.WriteLine("[CaveSharp-OS] Engine shutdown.");
        }
    }
}

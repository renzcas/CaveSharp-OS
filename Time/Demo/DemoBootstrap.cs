using CaveSharpOS.Time.EngineState;
using CaveSharpOS.Time;

namespace CaveSharpOS.Time.Demo
{
    public static class DemoBootstrap
    {
        private static readonly CaveChamber Chamber = new();

        public static void Tick()
        {
            TimeEngine.Tick(Chamber.World, Chamber.Events, Chamber.Agents);
            HeartbeatPanel.Update(Chamber.World, Chamber.Events, Chamber.Agents);
        }

        public static void Run(int tickCount, TimeSpan tickDelay)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(tickCount);
            ArgumentOutOfRangeException.ThrowIfLessThan(tickDelay, TimeSpan.Zero);

            for (var tick = 0; tick < tickCount; tick++)
            {
                Tick();
                if (tickDelay > TimeSpan.Zero)
                    Thread.Sleep(tickDelay);
            }
        }
    }
}

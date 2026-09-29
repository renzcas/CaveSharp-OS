using CaveSharp.CTA;
using CaveSharp.Realm;

namespace CaveSharp.Core
{
    public class UpdateLoop
    {
        private bool _isRunning = false;
        private int _tickRate = 60;

        // CTA Subsystems
        private CTAEvolutionEngine _ctaEvolution = new();
        private CTAEventEngine _ctaEvents = new();

        // Realm Subsystems
        private WeatherSystem _weather = new();
        private HealingMap _healing = new();

        public UpdateLoop()
        {
            // Register one corruption organism for now
            _ctaEvolution.Register(new CTAEntity("CorruptionNode-01"));
        }

        public void Start()
        {
            _isRunning = true;
            Console.WriteLine("[*] CaveSharp-OS Core Engine Initialized with CTA & Realm Subsystems.");

            int tickCount = 0;
            while (_isRunning && tickCount < 5)
            {
                Tick();
                tickCount++;
                Thread.Sleep(1000 / _tickRate);
            }
        }

        private void Tick()
        {
            Console.WriteLine($"\n[Tick] Engine state updated at {DateTime.UtcNow}");

            // CTA Systems
            _ctaEvolution.Tick();
            _ctaEvents.Tick();

            // Realm Systems
            _weather.Tick();
            _healing.Tick();
        }
    }
}

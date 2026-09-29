using System;
using System.Threading;
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
        private CTABoss _boss;

        // Realm Subsystems
        private WeatherSystem _weather = new();
        private HealingMap _healing = new();
        private CorruptionMap _corruption = new();

        public UpdateLoop()
        {
            _boss = new CTABoss("Overlord-Alpha", 850);

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
            _boss.ExecuteTacticalDirective();

            // Realm Systems
            _weather.Tick();
            _healing.Tick();
            _corruption.Tick();
        }
    }
}

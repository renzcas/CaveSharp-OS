using CaveSharp.CTA;
using CaveSharp.Realm;

namespace CaveSharp.Core
{
    public class UpdateLoop
    {
        private bool _isRunning = false;
        private int _tickRate = 60;
        private CTABoss _boss;
        private WeatherSystem _weather;

        public UpdateLoop()
        {
            _boss = new CTABoss { Name = "Overlord-Alpha", CommandPower = 850 };
            _weather = new WeatherSystem();
        }

        public void Start()
        {
            _isRunning = true;
            Console.WriteLine("[*] CaveSharp-OS Core Engine Initialized with CTA & Realm Subsystems.");
            
            int tickCount = 0;
            while (_isRunning && tickCount < 2)
            {
                Tick();
                tickCount++;
                Thread.Sleep(1000 / _tickRate);
            }
        }

        private void Tick()
        {
            Console.WriteLine($"\n[Tick] Engine state updated at {DateTime.UtcNow}");
            _boss.ExecuteTacticalDirective();
            _weather.ShiftClimate();
        }
    }
}
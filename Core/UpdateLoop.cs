using System;
using System.Threading;
using CaveSharp.CTA;
using CaveSharp.Realm;
using CaveSharp.Creatures;
using CaveSharp.Factions;
using CaveSharp.MetaAI;
using CaveSharp.WebUI;

namespace CaveSharp.Core
{
    public class UpdateLoop
    {
        private bool _isRunning = false;
        private int _tickRate = 60;

        // CTA Subsystems
        private readonly CTAEvolutionEngine _ctaEvolution = new();
        private readonly CTAEventEngine _ctaEvents = new();
        private readonly CTABoss _boss = new("Overlord-Alpha", 850);

        // Realm Subsystems
        private readonly WeatherSystem _weather = new();
        private readonly HealingMap _healing = new();
        private readonly CorruptionMap _corruption = new();
        private readonly NonLinearJunctionDetector _junctions = new();
        private readonly SpatialRiftEngine _rifts;

        // Creature Subsystems
        private readonly BehaviorEngine _behavior;
        private readonly MovementEngine _movement = new();
        private readonly SensesEngine _senses;
        private readonly CombatEngine _combat = new();
        private readonly CreatureManager _creatures;

        // Faction Subsystem
        private readonly FactionEngine _factions;

        // MetaAI Overseer
        private readonly Overseer _overseer;

        // WebUI Heartbeat
        private readonly HeartbeatPanel _heartbeat;

        public UpdateLoop()
        {
            // Realm wiring
            _rifts = new SpatialRiftEngine(_junctions);

            // Creature wiring
            _behavior = new BehaviorEngine(_corruption, _junctions);
            _senses = new SensesEngine(_corruption, _junctions);
            _creatures = new CreatureManager(_behavior, _movement, _senses, _combat);

            // Sample creatures
            _creatures.Register(new Creature("TunnelRat"));
            _creatures.Register(new Creature("StoneCrawler"));
            _creatures.Register(new Creature("GlowMoth"));

            // Factions wiring
            _factions = new FactionEngine(_corruption, _junctions);
            var factionA = new Faction("Deep Dwellers");
            var factionB = new Faction("Surface Exiles");

            factionA.AddMember(new Creature("Dweller Scout"));
            factionB.AddMember(new Creature("Exile Raider"));

            _factions.RegisterFaction(factionA);
            _factions.RegisterFaction(factionB);

            // CTA wiring
            _ctaEvolution.Register(new CTAEntity("CorruptionNode-01"));

            // MetaAI Overseer
            _overseer = new Overseer(
                _ctaEvolution,
                _boss,
                _corruption,
                _junctions,
                _creatures,
                _factions
            );

            // WebUI Heartbeat
            _heartbeat = new HeartbeatPanel(
                _ctaEvolution,
                _boss,
                _corruption,
                _junctions,
                _creatures,
                _factions,
                _overseer
            );
        }

        public void Start()
        {
            _isRunning = true;
            Console.WriteLine("[*] CaveSharp-OS Core Engine Initialized with CTA, Realm, Creatures, Factions, MetaAI, WebUI.");

            int tickCount = 0;
            while (_isRunning && tickCount < 100)
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
            _junctions.Tick();
            _rifts.Tick();

            // Creatures
            _creatures.Tick();

            // Factions
            _factions.Tick();

            // MetaAI Overseer
            _overseer.Tick();

            // WebUI Heartbeat
            _heartbeat.Tick();
        }
    }
}

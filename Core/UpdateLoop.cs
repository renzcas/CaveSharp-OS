using System;
using System.Threading;

using CaveSharp.CTA;
using CaveSharp.Realm;
using CaveSharp.Creatures;
using CaveSharp.Factions;
using CaveSharp.MetaAI;
using CaveSharp.WebUI;
using CaveSharpOS.Systems;

namespace CaveSharp.Core
{
    public class UpdateLoop
    {
        private bool _isRunning = false;
        private int _tickRate = 60;

        // CTA Subsystems
        private readonly CTAEvolutionEngine _ctaEvolution = new();
        private readonly CTAEventEngine _ctaEvents = new();
        private readonly CTABoss _boss = new("Overlord-Alpha", 0f, 850);

        // Realm Subsystems
        private readonly WeatherSystem _weather = new();
        private readonly HealingMap _healing = new();
        private readonly CorruptionMap _corruption = new();
        private readonly NonLinearJunctionDetector _junctions = new();
        private readonly SpatialRiftEngine _rifts;

        // Creature Subsystems
        private readonly BehaviorEngine _behavior;
        private readonly MovementEngine _movement;
        private readonly SensesEngine _senses;
        private readonly CombatEngine _combat;
        private readonly CreatureManager _creatures;

        // Faction Subsystem
        private readonly FactionEngine _factions;

        // MetaAI Overseer
        private readonly Overseer _overseer;

        // WebUI Heartbeat
        private readonly HeartbeatPanel _heartbeat;
        private readonly StateLogger _stateLogger = new();

        public UpdateLoop()
        {
            // Realm wiring
            _rifts = new SpatialRiftEngine(_junctions);

            // Creature wiring
            _behavior = new BehaviorEngine(_corruption, _junctions);
            _movement = new MovementEngine(_corruption, _junctions);
            _senses = new SensesEngine(_corruption, _junctions);
            _combat = new CombatEngine();
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
            Console.WriteLine("[*] CaveSharp-OS Core Engine Initialized.");

            int tickCount = 0;
            while (_isRunning && tickCount < 100)
            {
                Tick(++tickCount);
                Thread.Sleep(1000 / _tickRate);
            }
        }

        private void Tick(int tick)
        {
            Console.WriteLine($"\n[Tick] Engine state updated at {DateTime.UtcNow}");

            float dt = 1f / _tickRate;

            // CTA Systems
            string directive = _boss.AutoDirective();
            Console.WriteLine($"[CTA] Boss directive: {directive}");

            // Realm Systems
            _weather.ShiftClimate();
            _healing.ApplyHealing();
            _corruption.Spread();
            _junctions.Scan();
            _rifts.Update();

            // Creatures
            _creatures.Tick(dt);

            // Factions
            _factions.ProcessFactionLogic();

            // MetaAI Overseer
            _overseer.Process();

            _stateLogger.Log(tick, new
            {
                CtaOrganisms = _ctaEvolution.TotalOrganisms,
                BossAggression = _boss.AggressionLevel,
                BossPower = _boss.Power,
                CorruptionIntensity = _corruption.SpreadIntensity,
                JunctionOpen = _junctions.JunctionOpen,
                CreatureCount = _creatures.GetAll().Count,
                FactionCount = _factions.GetAll().Count,
                GlobalThreatLevel = _overseer.GlobalThreatLevel
            }, nameof(UpdateLoop));

            // WebUI Heartbeat
            _heartbeat.Refresh();
        }
    }
}

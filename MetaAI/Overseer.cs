using System;
using System.Threading.Tasks;
using CaveSharp.CTA;
using CaveSharp.Realm;
using CaveSharp.Creatures;
using CaveSharp.Factions;
using CaveSharp.Telemetry;

namespace CaveSharp.MetaAI
{
    public class Overseer
    {
        private readonly CTAEvolutionEngine _ctaEvolution;
        private readonly CTABoss _boss;
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;
        private readonly CreatureManager _creatures;
        private readonly FactionEngine _factions;

        private readonly CyberDefenseAgent _defense;
        private readonly WebSocketServer _telemetry;
        private readonly CTACommandServer _ctaCommands;

        public int GlobalThreatLevel { get; private set; } = 0;

        public Overseer(
            CTAEvolutionEngine ctaEvolution,
            CTABoss boss,
            CorruptionMap corruption,
            NonLinearJunctionDetector junctions,
            CreatureManager creatures,
            FactionEngine factions)
        {
            _ctaEvolution = ctaEvolution;
            _boss = boss;
            _corruption = corruption;
            _junctions = junctions;
            _creatures = creatures;
            _factions = factions;

            _defense = new CyberDefenseAgent();

            _telemetry = new WebSocketServer();
            _ = _telemetry.Start();

            _ctaCommands = new CTACommandServer(this);
            _ = _ctaCommands.Start();
        }

        /// <summary>
        /// Called by UpdateLoop.cs
        /// </summary>
        public void Process()
        {
            int threat = 0;

            // CTA threat
            threat += _ctaEvolution.TotalOrganisms * 2;
            threat += _boss.Power / 50;

            // Realm threat
            threat += _corruption.SpreadIntensity;
            if (_junctions.JunctionOpen) threat += 10;

            // Creature threat
            threat += CountLowHealthCreatures() / 2;

            // Faction threat
            threat += CountWeakFactions();

            GlobalThreatLevel = threat;

            Console.WriteLine($"[Overseer] GlobalThreatLevel={GlobalThreatLevel}");

            AdjustWorldParameters();

            // Defense agent
            _defense.Decay(0.1f);
            _defense.AnalyzeLog("unauthorized access attempt");

            // Broadcast telemetry
            _ = BroadcastState();
        }

        private int CountLowHealthCreatures()
        {
            int count = 0;
            foreach (var c in _creatures.GetAll())
                if (c.Health < 40) count++;
            return count;
        }

        private int CountWeakFactions()
        {
            int count = 0;
            foreach (var f in _factions.GetAll())
                if (f.Influence < 10) count++;
            return count;
        }

        private void AdjustWorldParameters()
        {
            if (GlobalThreatLevel > 40)
            {
                _corruption.IncreasePressure();
                Console.WriteLine("[Overseer] Increasing corruption pressure.");
            }

            if (GlobalThreatLevel > 60)
            {
                _boss.IncreaseAggression();
                Console.WriteLine("[Overseer] CTA Boss aggression increased.");
            }

            if (GlobalThreatLevel > 80)
            {
                Console.WriteLine("[Overseer] *** WORLD CRISIS THRESHOLD REACHED ***");
            }
        }

        public async Task BroadcastState()
        {
            var creatureSnapshot = _creatures.GetSnapshot();
            var corruptionGrid = _corruption.GetGrid();

            string json = System.Text.Json.JsonSerializer.Serialize(new
            {
                threat = GlobalThreatLevel,
                cta = _boss.AggressionLevel,
                corruption = _corruption.SpreadIntensity,
                creatures = creatureSnapshot,
                ctaBoss = _boss.Name,
                directive = GetDirective(),
                corruptionGrid = corruptionGrid
            });

            await _telemetry.SendToAll(json);
        }

        private string GetDirective()
        {
            if (_boss.AggressionLevel > 50)
                return "Deploy pressure units";

            return "Monitor corruption nodes";
        }

        // === CTA COMMAND HOOKS ===

        public void IncreaseCTA(float amount)
        {
            _boss.IncreaseAggression(amount);
            Console.WriteLine($"[Overseer] CTA increased by {amount}");
        }

        public void CooldownCTA(float amount)
        {
            _boss.CoolDown(amount);
            Console.WriteLine($"[Overseer] CTA cooled by {amount}");
        }

        public void SetCTADirective(string directive)
        {
            Console.WriteLine($"[Overseer] Directive received: {directive}");
        }
    }
}

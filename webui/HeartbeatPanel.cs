using System;
using CaveSharp.CTA;
using CaveSharp.Realm;
using CaveSharp.Creatures;
using CaveSharp.Factions;
using CaveSharp.MetaAI;

namespace CaveSharp.WebUI
{
    public class HeartbeatPanel
    {
        private readonly CTAEvolutionEngine _ctaEvolution;
        private readonly CTABoss _boss;
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;
        private readonly CreatureManager _creatures;
        private readonly FactionEngine _factions;
        private readonly Overseer _overseer;

        private int _tickCount = 0;

        public HeartbeatPanel(
            CTAEvolutionEngine ctaEvolution,
            CTABoss boss,
            CorruptionMap corruption,
            NonLinearJunctionDetector junctions,
            CreatureManager creatures,
            FactionEngine factions,
            Overseer overseer)
        {
            _ctaEvolution = ctaEvolution;
            _boss = boss;
            _corruption = corruption;
            _junctions = junctions;
            _creatures = creatures;
            _factions = factions;
            _overseer = overseer;
        }

        public void Tick()
        {
            _tickCount++;

            Console.WriteLine("\n=== [WebUI Heartbeat] ===");
            Console.WriteLine($"Tick: {_tickCount}");

            // CTA
            Console.WriteLine($"CTA Organisms: {_ctaEvolution.TotalOrganisms}");
            Console.WriteLine($"CTA Boss Aggression: {_boss.AggressionLevel}");

            // Realm
            Console.WriteLine($"Corruption Intensity: {_corruption.SpreadIntensity}");
            Console.WriteLine($"Junction Open: {_junctions.JunctionOpen}");

            // Creatures
            Console.WriteLine($"Creature Count: {_creatures.GetAll().Count}");

            // Factions
            foreach (var faction in _factions.GetAll())
            {
                Console.WriteLine($"Faction {faction.Name} Influence: {faction.Influence}");
            }

            // MetaAI Overseer
            Console.WriteLine($"Global Threat Level: {_overseer.GlobalThreatLevel}");

            // Pulse indicator
            Console.WriteLine($"Pulse: {( _tickCount % 2 == 0 ? "●" : "○" )}");
        }
    }
}

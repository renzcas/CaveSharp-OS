using System;
using CaveSharp.CTA;
using CaveSharp.Realm;
using CaveSharp.Creatures;
using CaveSharp.Factions;

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
        }

        public void Tick()
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
    }
}

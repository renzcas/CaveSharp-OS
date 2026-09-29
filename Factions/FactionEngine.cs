using System;
using System.Collections.Generic;
using CaveSharp.Creatures;
using CaveSharp.Realm;

namespace CaveSharp.Factions
{
    public class FactionEngine
    {
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;

        private readonly List<Faction> _factions = new();

        public FactionEngine(CorruptionMap corruption, NonLinearJunctionDetector junctions)
        {
            _corruption = corruption;
            _junctions = junctions;
        }

        public void RegisterFaction(Faction faction)
        {
            _factions.Add(faction);
            Console.WriteLine($"[FactionEngine] Registered faction: {faction.Name}");
        }

        public IReadOnlyList<Faction> GetAll() => _factions;

        public void Tick()
        {
            foreach (var faction in _factions)
            {
                // Corruption affects faction stability
                if (_corruption.SpreadIntensity > 6)
                {
                    faction.AdjustInfluence(-1);
                    Console.WriteLine($"[Faction] {faction.Name} loses stability due to corruption.");
                }

                // Dimensional instability affects influence
                if (_junctions.JunctionOpen)
                {
                    faction.AdjustInfluence(-2);
                    Console.WriteLine($"[Faction] {faction.Name} destabilized by junction anomaly.");
                }

                // Natural growth
                faction.AdjustInfluence(Random.Shared.Next(0, 2));
            }

            // Faction conflict check
            if (_factions.Count > 1)
            {
                var f1 = _factions[0];
                var f2 = _factions[1];

                if (f1.Influence > f2.Influence + 5)
                {
                    Console.WriteLine($"[FactionConflict] {f1.Name} dominates {f2.Name}.");
                }
                else if (f2.Influence > f1.Influence + 5)
                {
                    Console.WriteLine($"[FactionConflict] {f2.Name} dominates {f1.Name}.");
                }
            }
        }
    }
}

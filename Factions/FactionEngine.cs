using System;
using System.Collections.Generic;
using CaveSharp.Creatures;
using CaveSharp.Realm;

namespace CaveSharp.Factions
{
    /// <summary>
    /// Handles faction influence, corruption pressure,
    /// member behavior, and world‑state alignment.
    /// </summary>
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
        }

        public IReadOnlyList<Faction> GetAll() => _factions;

        /// <summary>
        /// REQUIRED by UpdateLoop.cs
        /// </summary>
        public void ProcessFactionLogic()
        {
            foreach (var faction in _factions)
            {
                float influence = faction.Influence;

                // Corruption reduces influence
                influence -= _corruption.SpreadIntensity * 0.01f;

                // Junction instability reduces influence
                if (_junctions.JunctionOpen)
                    influence -= 2f;

                // Members with low health reduce influence
                foreach (var member in faction.Members)
                {
                    if (member.Health < 40)
                        influence -= 0.5f;
                }

                // Clamp
                if (influence < 0) influence = 0;
                if (influence > 100) influence = 100;

                faction.Influence = influence;

                Console.WriteLine($"[FactionEngine] {faction.Name} influence={faction.Influence}");
            }
        }
    }
}

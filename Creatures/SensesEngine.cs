using System;
using System.Collections.Generic;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature perception:
    /// - Detect corruption hotspots
    /// - Detect nearby creatures
    /// - Trigger fear or aggression responses
    /// </summary>
    public class SensesEngine
    {
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;

        public SensesEngine(CorruptionMap corruption, NonLinearJunctionDetector junctions)
        {
            _corruption = corruption;
            _junctions = junctions;
        }

        public void Tick(Creature creature, IReadOnlyList<Creature> allCreatures)
        {
            if (creature.Health <= 0)
                return;

            // === CORRUPTION SENSE ===
            float corruptionLevel = _corruption.Sample(creature.X, creature.Y);

            if (corruptionLevel > 0.4f)
            {
                creature.BecomeAfraid("Sensed corruption hotspot");
                creature.IncreaseAggression(1);
            }

            // === JUNCTION SENSE ===
            if (_junctions.JunctionOpen)
            {
                creature.BecomeAfraid("Dimensional instability detected");
            }

            // === CREATURE PROXIMITY ===
            foreach (var other in allCreatures)
            {
                if (other == creature || other.Health <= 0)
                    continue;

                float dx = other.X - creature.X;
                float dy = other.Y - creature.Y;
                float dist = MathF.Sqrt(dx * dx + dy * dy);

                if (dist < 1.5f)
                {
                    creature.IncreaseAggression(2);
                }
            }
        }
    }
}

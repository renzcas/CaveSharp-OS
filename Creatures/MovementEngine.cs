using System;
using System.Collections.Generic;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature movement influenced by corruption fields,
    /// fear, aggression, and environmental instability.
    /// </summary>
    public class MovementEngine
    {
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;

        public MovementEngine(CorruptionMap corruption, NonLinearJunctionDetector junctions)
        {
            _corruption = corruption;
            _junctions = junctions;
        }

        public void Tick(Creature creature)
        {
            if (creature.Health <= 0)
                return;

            float baseSpeed = 0.1f;

            // Corruption pushes creatures away from hotspots
            float corruptionLevel = _corruption.Sample(creature.X, creature.Y);
            if (corruptionLevel > 0.3f)
            {
                creature.BecomeAfraid("Corruption hotspot detected");
                creature.IncreaseAggression(1);

                // Move away from corruption
                creature.X -= 0.05f * corruptionLevel;
                creature.Y -= 0.05f * corruptionLevel;
            }

            // Junction instability causes erratic movement
            if (_junctions.JunctionOpen)
            {
                creature.X += (float)(Math.Sin(DateTime.UtcNow.Millisecond) * 0.02f);
                creature.Y += (float)(Math.Cos(DateTime.UtcNow.Millisecond) * 0.02f);
            }

            // Normal wandering
            creature.X += baseSpeed * 0.5f;
            creature.Y += baseSpeed * 0.3f;
        }
    }
}

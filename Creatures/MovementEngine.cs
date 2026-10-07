using System;
using System.Collections.Generic;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature movement, fleeing, wandering, and corruption avoidance.
    /// BehaviorEngine decides emotional state; MovementEngine decides physical motion.
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

            float dx = 0;
            float dy = 0;

            // If afraid → flee randomly
            if (creature.IsAfraid)
            {
                dx = Random.Shared.Next(-2, 3);
                dy = Random.Shared.Next(-2, 3);

                Console.WriteLine($"[Movement] {creature.Name} flees to ({creature.X + dx},{creature.Y + dy})");
            }
            else
            {
                // Wander calmly
                dx = Random.Shared.Next(-1, 2);
                dy = Random.Shared.Next(-1, 2);

                Console.WriteLine($"[Movement] {creature.Name} wanders to ({creature.X + dx},{creature.Y + dy})");
            }

            // Apply movement
            creature.X += dx;
            creature.Y += dy;

            // Corruption exposure increases if creature steps into corruption zones
            float corruptionHere = _corruption.Sample(creature.X, creature.Y);
            if (corruptionHere > 0.1f)
            {
                creature.CorruptionExposure += corruptionHere * 0.5f;
                Console.WriteLine($"[Movement] {creature.Name} exposed to corruption: +{corruptionHere * 0.5f:F2}");
            }

            // Junction instability causes disorientation
            if (_junctions.JunctionOpen && Random.Shared.Next(0, 100) > 70)
            {
                creature.BecomeAfraid("dimensional instability disorientation");
            }
        }
    }
}

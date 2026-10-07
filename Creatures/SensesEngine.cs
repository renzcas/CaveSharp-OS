using System;
using System.Collections.Generic;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature perception: detecting nearby creatures,
    /// sensing corruption, reacting to junction instability,
    /// and triggering fear or aggression responses.
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

            // Sense corruption in the environment
            float corruptionHere = _corruption.Sample(creature.X, creature.Y);
            if (corruptionHere > 0.2f)
            {
                creature.CorruptionExposure += corruptionHere * 0.3f;
                Console.WriteLine($"[Senses] {creature.Name} senses corruption: +{corruptionHere * 0.3f:F2}");
            }

            // Sense junction instability
            if (_junctions.JunctionOpen && Random.Shared.Next(0, 100) > 50)
            {
                creature.BecomeAfraid("dimensional vibrations");
            }

            // Sense nearby creatures
            foreach (var other in allCreatures)
            {
                if (other == creature || other.Health <= 0)
                    continue;

                float dx = other.X - creature.X;
                float dy = other.Y - creature.Y;
                float dist = MathF.Sqrt(dx * dx + dy * dy);

                // Close proximity → tension
                if (dist < 3f)
                {
                    creature.IncreaseAggression(1);
                    Console.WriteLine($"[Senses] {creature.Name} feels tension near {other.Name}");
                }

                // Very close → fear or aggression spike
                if (dist < 1.5f)
                {
                    if (creature.Aggression < 20)
                        creature.BecomeAfraid($"close proximity to {other.Name}");
                    else
                        creature.IncreaseAggression(3);
                }
            }
        }
    }
}

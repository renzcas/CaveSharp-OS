using System;
using System.Collections.Generic;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    public class SensesEngine
    {
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;

        public int PerceptionRadius { get; } = 5;

        public SensesEngine(CorruptionMap corruption, NonLinearJunctionDetector junctions)
        {
            _corruption = corruption;
            _junctions = junctions;
        }

        public void Tick(Creature creature, List<Creature> nearbyCreatures)
        {
            if (creature.Health <= 0)
                return;

            // Detect corruption intensity
            if (_corruption.SpreadIntensity > 7)
            {
                creature.BecomeAfraid("high corruption intensity sensed");
            }

            // Detect dimensional instability
            if (_junctions.JunctionOpen)
            {
                creature.BecomeAfraid("dimensional junction instability sensed");
            }

            // Detect nearby creatures
            foreach (var other in nearbyCreatures)
            {
                if (other == creature)
                    continue;

                int dx = Math.Abs(other.X - creature.X);
                int dy = Math.Abs(other.Y - creature.Y);

                if (dx <= PerceptionRadius && dy <= PerceptionRadius)
                {
                    Console.WriteLine($"[Senses] {creature.Name} detects {other.Name} nearby.");
                }
            }
        }
    }
}

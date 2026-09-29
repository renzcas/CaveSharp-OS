using System;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    public class BehaviorEngine
    {
        private readonly CorruptionMap _corruption;
        private readonly NonLinearJunctionDetector _junctions;

        public BehaviorEngine(CorruptionMap corruption, NonLinearJunctionDetector junctions)
        {
            _corruption = corruption;
            _junctions = junctions;
        }

        public void Tick(Creature creature)
        {
            if (creature.Health <= 0)
                return;

            // Fear from corruption
            if (_corruption.SpreadIntensity > 5 && Random.Shared.Next(0, 100) > 60)
            {
                creature.BecomeAfraid("corruption spreading");
            }

            // Fear from junction anomalies
            if (_junctions.JunctionOpen && Random.Shared.Next(0, 100) > 40)
            {
                creature.BecomeAfraid("dimensional instability");
            }

            // Behavior decisions
            if (creature.IsAfraid)
            {
                Console.WriteLine($"[Behavior] {creature.Name} flees deeper into the cave.");
            }
            else if (creature.Hunger > 50)
            {
                Console.WriteLine($"[Behavior] {creature.Name} searches for food.");
            }
            else
            {
                Console.WriteLine($"[Behavior] {creature.Name} wanders calmly.");
            }
        }
    }
}

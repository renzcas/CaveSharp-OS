using System;
using CaveSharp.Realm;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature decision-making:
    /// fear, hunger-driven aggression, corruption panic,
    /// and basic behavioral state transitions.
    /// </summary>
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

            // Hunger → aggression
            if (creature.Hunger > 70)
            {
                creature.IncreaseAggression(2);
            }

            // Corruption → fear
            if (_corruption.SpreadIntensity > 5 && !creature.IsAfraid)
            {
                creature.BecomeAfraid("corruption spreading");
            }

            // Junction instability → fear
            if (_junctions.JunctionOpen && !creature.IsAfraid)
            {
                creature.BecomeAfraid("dimensional instability");
            }

            // Low health → fear
            if (creature.Health < 30 && !creature.IsAfraid)
            {
                creature.BecomeAfraid("critical health");
            }

            // Aggression spikes → reckless behavior
            if (creature.Aggression > 60)
            {
                Console.WriteLine($"[BehaviorEngine] {creature.Name} enters a reckless state.");
            }
        }
    }
}

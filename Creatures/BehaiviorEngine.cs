using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature decision-making:
    /// fear, hunger-driven aggression, corruption panic,
    /// and basic behavioral state transitions.
    /// </summary>
    public class BehaviorEngine
    {
        public void Tick(Creature creature)
        {
            // Hunger → aggression
            if (creature.Hunger > 70)
            {
                creature.IncreaseAggression(2);
            }

            // Corruption → fear
            if (creature.CorruptionExposure > 40 && !creature.IsAfraid)
            {
                creature.BecomeAfraid("Corruption exposure");
            }

            // Low health → fear
            if (creature.Health < 30 && !creature.IsAfraid)
            {
                creature.BecomeAfraid("Critical health");
            }

            // Aggression spikes → reckless behavior
            if (creature.Aggression > 60)
            {
                Console.WriteLine($"[BehaviorEngine] {creature.Name} enters a reckless state.");
            }

            // Calm down slowly if safe
            if (creature.Aggression < 10 && creature.IsAfraid == false)
            {
                // mild natural calm
            }
        }
    }
}

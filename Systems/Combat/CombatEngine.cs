using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature combat interactions:
    /// aggression-based attacks, fear-based retreats,
    /// and corruption-influenced violence spikes.
    /// </summary>
    public class CombatEngine
    {
        public void Tick(Creature creature, IReadOnlyList<Creature> allCreatures)
        {
            if (creature.Health <= 0)
                return;

            // Afraid creatures do not fight
            if (creature.IsAfraid)
                return;

            // Low aggression → no combat
            if (creature.Aggression < 20)
                return;

            // Find nearby targets
            Creature? target = FindClosestTarget(creature, allCreatures);
            if (target == null)
                return;

            float dx = target.X - creature.X;
            float dy = target.Y - creature.Y;
            float dist = MathF.Sqrt(dx * dx + dy * dy);

            // Too far → no combat
            if (dist > 2f)
                return;

            // Attack!
            int damage = CalculateDamage(creature);
            target.Damage(damage);

            Console.WriteLine($"[Combat] {creature.Name} attacks {target.Name} for {damage} damage!");

            // Aggression increases after attacking
            creature.IncreaseAggression(3);

            // Corruption exposure increases violence
            if (creature.CorruptionExposure > 30)
            {
                creature.IncreaseAggression(5);
                Console.WriteLine($"[Combat] {creature.Name} becomes more violent due to corruption.");
            }
        }

        private Creature? FindClosestTarget(Creature creature, IReadOnlyList<Creature> all)
        {
            Creature? closest = null;
            float closestDist = float.MaxValue;

            foreach (var other in all)
            {
                if (other == creature || other.Health <= 0)
                    continue;

                float dx = other.X - creature.X;
                float dy = other.Y - creature.Y;
                float dist = MathF.Sqrt(dx * dx + dy * dy);

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = other;
                }
            }

            return closest;
        }

        private int CalculateDamage(Creature creature)
        {
            // Base damage from aggression
            int dmg = (int)(creature.Aggression * 0.3f);

            // Corruption amplifies violence
            dmg += (int)(creature.CorruptionExposure * 0.1f);

            if (dmg < 1)
                dmg = 1;

            return dmg;
        }
    }
}

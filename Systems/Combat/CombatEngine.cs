using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    public class CombatEngine
    {
        public void Tick(Creature creature, IReadOnlyList<Creature> allCreatures)
        {
            if (creature.Health <= 0)
                return;

            if (creature.IsAfraid)
                return;

            if (creature.Aggression < 20)
                return;

            Creature? target = FindClosestTarget(creature, allCreatures);
            if (target == null)
                return;

            float dx = target.X - creature.X;
            float dy = target.Y - creature.Y;
            float dist = MathF.Sqrt(dx * dx + dy * dy);

            if (dist > 2f)
                return;

            int damage = CalculateDamage(creature);
            target.Health -= damage;

            Console.WriteLine($"[Combat] {creature.Name} attacks {target.Name} for {damage} damage!");
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
            int dmg = (int)(creature.Aggression * 0.3f);
            dmg += (int)(creature.CorruptionExposure * 0.1f);
            return Math.Max(dmg, 1);
        }
    }
}

using System;

namespace CaveSharp.Creatures
{
    public class MovementEngine
    {
        public void Tick(Creature creature)
        {
            if (creature.Health <= 0)
                return;

            // Basic movement logic
            int dx = Random.Shared.Next(-1, 2);
            int dy = Random.Shared.Next(-1, 2);

            creature.X += dx;
            creature.Y += dy;

            Console.WriteLine($"[Movement] {creature.Name} moves to ({creature.X}, {creature.Y}).");

            // Fear-based fleeing
            if (creature.IsAfraid)
            {
                creature.X += Random.Shared.Next(1, 3);
                creature.Y += Random.Shared.Next(1, 3);

                Console.WriteLine($"[Movement] {creature.Name} flees to ({creature.X}, {creature.Y}).");
            }

            // Hunger-based searching
            if (creature.Hunger > 60)
            {
                creature.X += Random.Shared.Next(-1, 2);
                creature.Y += Random.Shared.Next(-1, 2);

                Console.WriteLine($"[Movement] {creature.Name} searches for food near ({creature.X}, {creature.Y}).");
            }
        }
    }
}

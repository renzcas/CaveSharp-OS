using System;

namespace CaveSharp.Creatures
{
    public class Creature
    {
        public string Name { get; }

        // Biological state
        public int Health { get; private set; } = 100;
        public int Hunger { get; private set; } = 0;
        public bool IsAfraid { get; private set; } = false;

        // Spatial state
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Creature(string name)
        {
            Name = name;
        }

        public void BecomeAfraid(string reason)
        {
            IsAfraid = true;
            Console.WriteLine($"[Creature] {Name} becomes afraid due to {reason}.");
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"[Creature] {Name} takes {amount} damage. Health={Health}");

            if (Health <= 0)
                Console.WriteLine($"[Creature] {Name} has died.");
        }

        public void Tick()
        {
            // Hunger increases every tick
            Hunger += Random.Shared.Next(1, 4);

            // Starvation damage
            if (Hunger > 80)
            {
                Health -= Random.Shared.Next(1, 3);
                Console.WriteLine($"[Creature] {Name} is starving. Health={Health}");
            }

            // Death check
            if (Health <= 0)
            {
                Console.WriteLine($"[Creature] {Name} has died.");
                return;
            }

            // Random fear reaction
            if (Random.Shared.Next(0, 100) > 85)
            {
                IsAfraid = true;
                Console.WriteLine($"[Creature] {Name} senses danger and becomes afraid.");
            }
            else
            {
                IsAfraid = false;
            }

            Console.WriteLine(
                $"[Creature] {Name} Tick: Health={Health}, Hunger={Hunger}, Afraid={IsAfraid}, Pos=({X},{Y})"
            );
        }
    }
}

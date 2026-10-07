using System;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Base creature model used throughout CaveSharp-OS.
    /// Overseer, Factions, Realm, and WebUI all read from this class.
    /// </summary>
    public class Creature
    {
        public string Id { get; }
        public string Name { get; }

        public int Health { get; private set; }
        public int Aggression { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public bool IsAlive => Health > 0;

        public Creature(string name, int x, int y)
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            Name = name;

            Health = 100;
            Aggression = 0;

            X = x;
            Y = y;
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;

            Console.WriteLine($"[Creature] {Name} took {amount} damage. HP={Health}");
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > 100)
                Health = 100;

            Console.WriteLine($"[Creature] {Name} healed {amount}. HP={Health}");
        }

        public void IncreaseAggression(int amount)
        {
            Aggression += amount;
            if (Aggression > 100)
                Aggression = 100;

            Console.WriteLine($"[Creature] {Name} aggression increased to {Aggression}");
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;

            Console.WriteLine($"[Creature] {Name} moved to ({X},{Y})");
        }
    }
}

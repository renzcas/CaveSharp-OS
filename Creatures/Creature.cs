using System;

namespace CaveSharp.Creatures
{
    public class Creature
    {
        // Identity
        public string Name { get; }

        // Biological stats
        public float Health { get; set; }
        public float Aggression { get; set; }
        public float Hunger { get; set; }
        public float CorruptionExposure { get; set; }

        // Position (required by MovementEngine, SensesEngine, CombatEngine)
        public float X { get; set; }
        public float Y { get; set; }

        // Fear system
        public bool IsAfraid { get; private set; }

        public Creature(string name, float health = 100f)
        {
            Name = name;

            Health = health;
            Aggression = 0f;
            Hunger = 0f;
            CorruptionExposure = 0f;

            X = 0f;
            Y = 0f;

            IsAfraid = false;
        }

        public void BecomeAfraid(string reason = "")
        {
            IsAfraid = true;
            Console.WriteLine($"[Creature] {Name} becomes afraid. {reason}");
        }

        public void Tick(float dt)
        {
            // Hunger increases naturally
            Hunger += dt * 0.5f;
            if (Hunger > 100f)
                Hunger = 100f;

            // Biological decay
            Health -= dt * 0.1f;
            if (Health < 0f)
                Health = 0f;

            // Aggression decay
            Aggression -= dt * 0.05f;
            if (Aggression < 0f)
                Aggression = 0f;

            // Corruption decay
            CorruptionExposure -= dt * 0.02f;
            if (CorruptionExposure < 0f)
                CorruptionExposure = 0f;

            // Fear resets slowly
            if (IsAfraid && Aggression < 5f)
                IsAfraid = false;
        }
    }
}

using System;

namespace CaveSharp.CTA
{
    /// <summary>
    /// A single CTA organism/entity. Evolves corruption over time.
    /// CTAEvolutionEngine drives corruption growth.
    /// Overseer reads corruption levels for threat calculations.
    /// </summary>
    public class CTAEntity
    {
        public string Id { get; }
        public string Name { get; }

        public int CorruptionLevel { get; private set; }
        public int Aggression { get; private set; }
        public int Health { get; private set; }
        public bool IsActive { get; private set; }

        public CTAEntity(string name)
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
            Name = name;

            CorruptionLevel = 1;
            Aggression = 1;
            Health = 100;
            IsActive = true;
        }

        public void IncreaseCorruption(int amount)
        {
            CorruptionLevel += amount;
            if (CorruptionLevel > 100)
                CorruptionLevel = 100;

            Aggression += amount / 2;
            if (Aggression > 100)
                Aggression = 100;

            // Corruption damages the organism
            Health -= amount;
            if (Health <= 0)
                Deactivate();

            Console.WriteLine($"[CTAEntity] {Name} corruption={CorruptionLevel}, aggression={Aggression}, health={Health}");
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
                Deactivate();
        }

        public void Deactivate()
        {
            IsActive = false;
            Health = 0;
            Console.WriteLine($"[CTAEntity] {Name} has been neutralized.");
        }
    }
}

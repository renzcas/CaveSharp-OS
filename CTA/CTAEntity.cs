namespace CaveSharp.CTA
{
    public class CTAEntity
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

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
            Aggression += amount / 2;

            // Corruption damages the organism
            Health -= amount;

            if (Health <= 0)
                Deactivate();
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}

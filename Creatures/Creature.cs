namespace CaveSharp.Creatures
{
    public class Creature
    {
        public string Name { get; }
        public float Health { get; private set; }
        public float Aggression { get; private set; }
        public Vector2 Position { get; private set; }
        public float CorruptionExposure { get; private set; }

        public Creature(string name, float health = 100f)
        {
            Name = name;
            Health = health;
            Aggression = 0f;
            Position = new Vector2(0, 0);
            CorruptionExposure = 0f;
        }

        public void Update(float dt)
        {
            // Example biological decay
            Health -= dt * 0.1f;
            if (Health < 0) Health = 0;

            // Example aggression decay
            Aggression -= dt * 0.05f;
            if (Aggression < 0) Aggression = 0;

            // Example corruption exposure decay
            CorruptionExposure -= dt * 0.02f;
            if (CorruptionExposure < 0) CorruptionExposure = 0;
        }
    }
}

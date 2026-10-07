using System;

namespace CaveSharpOS.Personas
{
    /// <summary>
    /// Generates and evolves persona traits, identity signatures,
    /// behavioral tendencies, and emergent personality patterns.
    /// This engine feeds into DialogueEngine, CTA, and creature AI.
    /// </summary>
    public class PersonaEngine
    {
        // Core persona traits (0–1)
        public float Boldness { get; private set; } = 0.5f;
        public float Curiosity { get; private set; } = 0.5f;
        public float Aggression { get; private set; } = 0.5f;

        /// <summary>
        /// Update persona traits over time.
        /// </summary>
        public void Update(float dt)
        {
            // Simple oscillation to keep personas dynamic
            float pulse = MathF.Sin(dt * 0.5f);

            Boldness = Clamp(0.5f + pulse * 0.2f);
            Curiosity = Clamp(0.5f + pulse * 0.15f);
            Aggression = Clamp(0.5f + pulse * 0.25f);

            // Example debug:
            // Console.WriteLine($"[PersonaEngine] B={Boldness:0.00} C={Curiosity:0.00} A={Aggression:0.00}");
        }

        /// <summary>
        /// Returns a textual description of the persona.
        /// </summary>
        public string Describe()
        {
            return $"Boldness={Boldness:0.00}, Curiosity={Curiosity:0.00}, Aggression={Aggression:0.00}";
        }

        private float Clamp(float v)
        {
            if (v < 0f) return 0f;
            if (v > 1f) return 1f;
            return v;
        }
    }
}

using System;

namespace CaveSharpOS.Systems.Mythic
{
    /// <summary>
    /// Handles mythic memory retention, resonance imprinting,
    /// symbolic recall, and archetypal persistence.
    /// This engine stores and mutates mythic states over time.
    /// </summary>
    public class MythicMemoryEngine
    {
        // Represents accumulated mythic memory (0–1)
        public float MemoryLevel { get; private set; } = 0f;

        // Represents how quickly memory fades (0–1)
        public float DecayRate { get; set; } = 0.05f;

        /// <summary>
        /// Update mythic memory over time.
        /// </summary>
        public void Update(float dt)
        {
            // Memory slowly decays
            MemoryLevel -= dt * DecayRate;
            if (MemoryLevel < 0f)
                MemoryLevel = 0f;

            // Example debug:
            // Console.WriteLine($"[MythicMemoryEngine] Memory={MemoryLevel:0.00}");
        }

        /// <summary>
        /// Imprint new mythic resonance into memory.
        /// </summary>
        public void Imprint(float amount)
        {
            MemoryLevel += amount;
            if (MemoryLevel > 1f)
                MemoryLevel = 1f;
        }

        /// <summary>
        /// Returns a textual description of the current memory state.
        /// </summary>
        public string GetMemoryState()
        {
            if (MemoryLevel > 0.8f) return "Eternal Echo";
            if (MemoryLevel > 0.6f) return "Deep Imprint";
            if (MemoryLevel > 0.4f) return "Lingering Trace";
            if (MemoryLevel > 0.2f) return "Fading";
            return "Forgotten";
        }
    }
}

using System;

namespace CaveSharpOS.Systems.Mythic
{
    /// <summary>
    /// Handles symbolic transformations, glyph resonance,
    /// archetypal signatures, and meaning-flow dynamics.
    /// This engine interprets and mutates symbolic states
    /// that feed into narrative and mythic cycles.
    /// </summary>
    public class SymbolEngine
    {
        // Current symbolic resonance level (0–1)
        public float Resonance { get; private set; } = 0f;

        // Current glyph index (abstract symbolic state)
        public int Glyph { get; private set; } = 0;

        /// <summary>
        /// Update symbolic resonance and glyph transitions.
        /// </summary>
        public void Update(float dt)
        {
            // Resonance oscillates over time
            Resonance += dt * 0.15f;

            if (Resonance > 1f)
                Resonance = 0f;

            // Glyph transitions based on resonance thresholds
            if (Resonance < 0.2f) Glyph = 0;      // Seed
            else if (Resonance < 0.4f) Glyph = 1; // Echo
            else if (Resonance < 0.6f) Glyph = 2; // Fracture
            else if (Resonance < 0.8f) Glyph = 3; // Merge
            else Glyph = 4;                       // Ascend

            // Example debug output:
            // Console.WriteLine($"[SymbolEngine] Glyph={Glyph} Resonance={Resonance:0.00}");
        }

        /// <summary>
        /// Returns a textual description of the current glyph.
        /// </summary>
        public string GetGlyphName()
        {
            return Glyph switch
            {
                0 => "Seed",
                1 => "Echo",
                2 => "Fracture",
                3 => "Merge",
                4 => "Ascend",
                _ => "Unknown"
            };
        }
    }
}

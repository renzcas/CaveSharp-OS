using System;

namespace CaveSharpOS.Systems.Mythic
{
    /// <summary>
    /// Synthesizes archetypes, cycles, symbols, laws, and memory
    /// into a coherent mythic narrative flow. This engine produces
    /// mythic states that influence CTA, Overseer, and Realm behavior.
    /// </summary>
    public class MythicNarrativeEngine
    {
        public string CurrentTheme { get; private set; } = "Dormant";
        public float NarrativePulse { get; private set; } = 0f;

        /// <summary>
        /// Update the mythic narrative pulse.
        /// </summary>
        public void Update(float dt)
        {
            // Pulse oscillates over time
            NarrativePulse += dt * 0.12f;

            if (NarrativePulse > 1f)
                NarrativePulse = 0f;

            // Theme transitions based on pulse
            if (NarrativePulse < 0.2f) CurrentTheme = "Dormant";
            else if (NarrativePulse < 0.4f) CurrentTheme = "Awakening";
            else if (NarrativePulse < 0.6f) CurrentTheme = "Tension";
            else if (NarrativePulse < 0.8f) CurrentTheme = "Revelation";
            else CurrentTheme = "Mythic Surge";

            // Example debug:
            // Console.WriteLine($"[MythicNarrativeEngine] Theme={CurrentTheme} Pulse={NarrativePulse:0.00}");
        }

        /// <summary>
        /// Returns a textual description of the current narrative state.
        /// </summary>
        public string Describe()
        {
            return CurrentTheme switch
            {
                "Dormant" => "The myth sleeps beneath the surface.",
                "Awakening" => "Symbols stir and archetypes begin to move.",
                "Tension" => "Cycles collide and laws strain under pressure.",
                "Revelation" => "Memory ignites and truth emerges.",
                "Mythic Surge" => "The world is reshaped by narrative force.",
                _ => "Unknown mythic state."
            };
        }
    }
}

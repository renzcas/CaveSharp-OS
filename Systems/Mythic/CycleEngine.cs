using System;

namespace CaveSharpOS.Systems.Mythic
{
    /// <summary>
    /// Handles mythic cycle progression:
    /// - Birth → Growth → Conflict → Transformation → Renewal
    /// - Drives narrative arcs and symbolic transitions
    /// </summary>
    public class CycleEngine
    {
        public float Phase { get; private set; } = 0f;

        /// <summary>
        /// Advance the mythic cycle over time.
        /// </summary>
        public void Update(float dt)
        {
            // Simple cycle progression
            Phase += dt * 0.1f;

            if (Phase > 1f)
                Phase = 0f;

            // Example placeholder logic:
            // Console.WriteLine($"[CycleEngine] Phase={Phase:0.00}");
        }

        /// <summary>
        /// Returns a textual description of the current mythic phase.
        /// </summary>
        public string GetPhaseName()
        {
            if (Phase < 0.2f) return "Birth";
            if (Phase < 0.4f) return "Growth";
            if (Phase < 0.6f) return "Conflict";
            if (Phase < 0.8f) return "Transformation";
            return "Renewal";
        }
    }
}

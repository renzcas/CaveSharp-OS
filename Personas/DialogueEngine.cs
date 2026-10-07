ueusing System;

namespace CaveSharpOS.Personas
{
    /// <summary>
    /// Generates dialogue lines, emotional tone, conversational state,
    /// and persona-driven speech patterns. This engine is used by CTA,
    /// creatures, factions, and mythic narrative systems.
    /// </summary>
    public class DialogueEngine
    {
        public string CurrentTone { get; private set; } = "Neutral";
        public float EmotionLevel { get; private set; } = 0.5f;

        /// <summary>
        /// Update dialogue emotional state over time.
        /// </summary>
        public void Update(float dt)
        {
            // Emotional oscillation
            float pulse = MathF.Sin(dt * 0.4f);
            EmotionLevel = Clamp(0.5f + pulse * 0.3f);

            // Tone selection
            if (EmotionLevel < 0.2f) CurrentTone = "Calm";
            else if (EmotionLevel < 0.4f) CurrentTone = "Soft";
            else if (EmotionLevel < 0.6f) CurrentTone = "Neutral";
            else if (EmotionLevel < 0.8f) CurrentTone = "Intense";
            else CurrentTone = "Fierce";

            // Example debug:
            // Console.WriteLine($"[DialogueEngine] Tone={CurrentTone} Emotion={EmotionLevel:0.00}");
        }

        /// <summary>
        /// Generate a dialogue line based on current tone.
        /// </summary>
        public string GenerateLine(string topic)
        {
            return CurrentTone switch
            {
                "Calm" => $"In quiet thought, they speak of {topic}.",
                "Soft" => $"With gentle voice, they mention {topic}.",
                "Neutral" => $"They discuss {topic} plainly.",
                "Intense" => $"Their words burn as they speak of {topic}.",
                "Fierce" => $"With blazing conviction, they declare {topic}!",
                _ => $"They speak of {topic}."
            };
        }

        private float Clamp(float v)
        {
            if (v < 0f) return 0f;
            if (v > 1f) return 1f;
            return v;
        }
    }
}

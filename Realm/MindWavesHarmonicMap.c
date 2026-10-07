using System;

namespace CaveSharp.Realm
{
    /// <summary>
    /// Computes harmonic resonance, ambience shifts,
    /// and light distortion for RealmRegion objects.
    /// </summary>
    public class MindWavesRealmHarmonics
    {
        public void Process(RealmRegion region)
        {
            // Base harmonic oscillation
            float t = (float)(DateTime.UtcNow.Millisecond / 1000.0);
            float wave = MathF.Sin(t * 6.28f); // full sine cycle

            // Harmonic level influenced by resonance
            region.HarmonicLevel = region.ResonanceFactor * (wave * 50f + 50f);

            // Light shift reacts to harmonic spikes
            region.LightShift = region.HarmonicLevel * 0.02f;

            // Ambience transitions
            if (region.HarmonicLevel < 20)
                region.Ambience = "Stillness";
            else if (region.HarmonicLevel < 40)
                region.Ambience = "Calm";
            else if (region.HarmonicLevel < 60)
                region.Ambience = "Flow";
            else if (region.HarmonicLevel < 80)
                region.Ambience = "Vivid";
            else
                region.Ambience = "Resonant";
        }
    }
}

using CaveSharp.Core;

namespace CaveSharp.Realm
{
    public class MindWavesRealmHarmonics
    {
        private readonly Kernel kernel;

        public MindWavesRealmHarmonics(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public void ApplyTo(RealmRegion region)
        {
            float c = kernel.MindWaves.Consciousness;

            // Environmental modulation based on wave-mode
            region.HarmonicLevel = c;

            if (c < -0.5f)
            {
                region.Ambience = "Stillness";        // D-wave
                region.LightShift = -0.2f;
            }
            else if (c < 0.5f)
            {
                region.Ambience = "Calm";             // A-wave
                region.LightShift = 0.0f;
            }
            else if (c < 1.5f)
            {
                region.Ambience = "Flow";             // B-wave
                region.LightShift = 0.1f;
            }
            else if (c < 3.0f)
            {
                region.Ambience = "Vivid";            // C-wave
                region.LightShift = 0.25f;
            }
            else
            {
                region.Ambience = "Resonant";         // Z-wave
                region.LightShift = 0.5f;
            }
        }
    }
}

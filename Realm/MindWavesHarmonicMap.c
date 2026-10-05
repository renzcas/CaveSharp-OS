using CaveSharp.Core;

namespace CaveSharp.Realm
{
    public class MindWavesHarmonicMap
    {
        private readonly Kernel kernel;

        public MindWavesHarmonicMap(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public float ComputeRegionHarmonic(RealmRegion region)
        {
            float c = kernel.MindWaves.Consciousness;

            // Harmonic intensity is consciousness * region resonance
            float intensity = c * region.ResonanceFactor;

            return Math.Clamp(intensity, 0f, 5f);
        }
    }
}

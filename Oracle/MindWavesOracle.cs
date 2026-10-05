using CaveSharp.Core;

namespace CaveSharp.Oracle
{
    public class MindWavesOracle
    {
        private readonly Kernel kernel;

        public MindWavesOracle(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public string Prophecy()
        {
            float c = kernel.MindWaves.Consciousness;

            if (c < -0.5f)
                return "The world sleeps beneath the delta veil.";

            if (c < 0.5f)
                return "Identity stabilizes. Paths remain predictable.";

            if (c < 1.5f)
                return "Routing energies converge. Movement accelerates.";

            if (c < 3.0f)
                return "Perception sharpens. The world reveals its edges.";

            return "Harmonics align. A portal awaits.";
        }
    }
}

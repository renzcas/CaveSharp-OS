using CaveSharp.Core;
using CaveSharp.Core.MindWaves;

namespace CaveSharp.Portals
{
    public class MindWavesPortalActivation
    {
        private readonly Kernel kernel;

        public float ZWaveThreshold = 3.0f;

        public MindWavesPortalActivation(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public bool IsPortalOpen()
        {
            float consciousness = kernel.MindWaves.Consciousness;

            return consciousness > ZWaveThreshold;
        }

        public string PortalState()
        {
            return IsPortalOpen() ? "OPEN" : "CLOSED";
        }
    }
}

using CaveSharp.Core;

namespace CaveSharp.MetaAI
{
    public class MindWavesMetaAI
    {
        private readonly Kernel kernel;

        public MindWavesMetaAI(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public string Decide()
        {
            float c = kernel.MindWaves.Consciousness;

            if (c < -0.5f)
                return "Hold position.";

            if (c < 0.5f)
                return "Maintain current strategy.";

            if (c < 1.5f)
                return "Explore new routes.";

            if (c < 3.0f)
                return "Engage with environment.";

            return "Initiate transcendence protocol.";
        }
    }
}

using CaveSharp.Core;

namespace CaveSharp.Portals
{
    public class MindWavesPortalScene
    {
        private readonly Kernel kernel;

        public MindWavesPortalScene(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public string Render()
        {
            float c = kernel.MindWaves.Consciousness;

            if (c > 3.0f)
                return "A shimmering vortex forms above the volcano lake.";

            if (c > 1.5f)
                return "The air vibrates with harmonic tension.";

            if (c > 0.5f)
                return "The cavern walls pulse faintly.";

            return "The portal remains dormant.";
        }
    }
}

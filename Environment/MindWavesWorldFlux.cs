using CaveSharp.Core;

namespace CaveSharp.Environment
{
    public class MindWavesWorldFlux
    {
        private readonly Kernel kernel;

        public MindWavesWorldFlux(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public float ComputeFlux(World world)
        {
            float c = kernel.MindWaves.Consciousness;

            // Flux increases with wave intensity
            float flux = world.BaseFlux + (c * 0.25f);

            return Math.Clamp(flux, 0f, world.MaxFlux);
        }
    }
}

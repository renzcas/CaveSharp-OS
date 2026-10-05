using CaveSharp.Core.MindWaves;

namespace CaveSharp.Core
{
    public class MindWavesTelemetry
    {
        private readonly Kernel kernel;

        public MindWavesTelemetry(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public string Report()
        {
            float c = kernel.MindWaves.Consciousness;

            string mode =
                c < -0.5f ? "D-Wave" :
                c < 0.5f  ? "A-Wave" :
                c < 1.5f  ? "B-Wave" :
                c < 3.0f  ? "C-Wave" :
                            "Z-Wave";

            return $"Consciousness: {c:F3} | Mode: {mode}";
        }
    }
}

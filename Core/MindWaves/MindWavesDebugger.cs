using CaveSharp.Core;

namespace CaveSharp.Core.MindWaves
{
    public class MindWavesDebugger
    {
        private readonly Kernel kernel;

        public MindWavesDebugger(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public void Print()
        {
            float c = kernel.MindWaves.Consciousness;

            string mode =
                c < -0.5f ? "D-Wave" :
                c < 0.5f  ? "A-Wave" :
                c < 1.5f  ? "B-Wave" :
                c < 3.0f  ? "C-Wave" :
                            "Z-Wave";

            Console.WriteLine($"[MindWaves] Consciousness={c:F3} Mode={mode}");
        }
    }
}

using CaveSharp.Core;
using CaveSharp.Core.MindWaves;



namespace CaveSharp.Creatures
{
    public class MindWavesCreatureIntegration
    {
        private readonly Kernel kernel;

        public MindWavesCreatureIntegration(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public void ApplyTo(ICreature creature)
        {
            float c = kernel.MindWaves.Consciousness;

            creature.MindState = c;

            // Behavioral modulation based on wave mode
            if (c < -0.5f)
            {
                creature.Behavior = "Dormant";      // D-wave
            }
            else if (c < 0.5f)
            {
                creature.Behavior = "Calm";         // A-wave
            }
            else if (c < 1.5f)
            {
                creature.Behavior = "Searching";    // B-wave
            }
            else if (c < 3.0f)
            {
                creature.Behavior = "Alert";        // C-wave
            }
            else
            {
                creature.Behavior = "Transcendent"; // Z-wave
            }
        }
    }
}

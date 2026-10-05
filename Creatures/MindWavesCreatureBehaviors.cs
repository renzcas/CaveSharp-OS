using CaveSharp.Core;

namespace CaveSharp.Creatures
{
    public class MindWavesCreatureBehaviors
    {
        private readonly Kernel kernel;

        public MindWavesCreatureBehaviors(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public void Update(ICreature creature)
        {
            float c = kernel.MindWaves.Consciousness;

            if (c < -0.5f)
            {
                creature.Action = "Sleep";
                creature.Alertness = 0.1f;
            }
            else if (c < 0.5f)
            {
                creature.Action = "Wander";
                creature.Alertness = 0.3f;
            }
            else if (c < 1.5f)
            {
                creature.Action = "Search";
                creature.Alertness = 0.6f;
            }
            else if (c < 3.0f)
            {
                creature.Action = "Hunt";
                creature.Alertness = 0.9f;
            }
            else
            {
                creature.Action = "Ascend";
                creature.Alertness = 1.0f;
            }
        }
    }
}

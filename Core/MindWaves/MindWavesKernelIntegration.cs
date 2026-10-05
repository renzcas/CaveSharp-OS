using System;
using CaveSharp.Core.MindWaves;

namespace CaveSharp.Core
{
    public class MindWavesKernelIntegration
    {
        private readonly MindWavesAgent agent = new MindWavesAgent();

        public float Consciousness => agent.Consciousness;

        public void Tick(float dt, float sensoryInput)
        {
            agent.SensoryInput = sensoryInput;
            agent.Update(dt);
        }
    }
}

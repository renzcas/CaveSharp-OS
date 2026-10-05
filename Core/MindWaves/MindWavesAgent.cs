using System;

namespace CaveSharp.Core.MindWaves
{
    public class MindWavesAgent
    {
        public float Consciousness { get; private set; }
        public float SensoryInput { get; set; }

        private readonly MindWavesCore core = new MindWavesCore();

        public void Update(float dt)
        {
            Consciousness = core.Step(dt, SensoryInput);
        }
    }
}

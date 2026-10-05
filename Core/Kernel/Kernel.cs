using System;
using CaveSharp.Core.MindWaves;

namespace CaveSharp.Core
{
    public class Kernel
    {
        public MindWavesKernelIntegration MindWaves { get; private set; }

        private float timeAccumulator = 0f;

        public Kernel()
        {
            MindWaves = new MindWavesKernelIntegration();
        }

        // Main OS tick — called every frame or update cycle
        public void Tick(float dt, float sensoryInput)
        {
            timeAccumulator += dt;

            // Update consciousness engine
            MindWaves.Tick(dt, sensoryInput);

            // Future: hook creatures, personas, portals, physics, etc.
            // Example:
            // Creatures.UpdateAll(dt, MindWaves.Consciousness);
            // Portals.CheckActivation(MindWaves.Consciousness);
        }
    }
}

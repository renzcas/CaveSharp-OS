using System;

namespace CaveSharp.Environment
{
    public class MindWavesWorldSensors
    {
        // Example: environmental noise, light, heat, magic, etc.
        public float ComputeSensoryField(World world)
        {
            float noise = world.NoiseLevel;
            float light = world.LightLevel;
            float magic = world.MagicFlux;
            float heat  = world.Temperature;

            // Normalize and blend into a single sensory input
            float sensory = (noise + light + magic + heat) * 0.25f;

            return Math.Clamp(sensory, 0f, 1f);
        }
    }
}

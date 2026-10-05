using System;

namespace CaveSharp.Core.MindWaves
{
    public class MindWavesCore
    {
        private float t = 0f;

        // Default frequencies for A/B/C waves
        public float AlphaBetaFreq = 12f;   // A-wave (carrier)
        public float BetaGammaFreq = 42f;   // B-wave (routing)
        public float GammaFreq = 72f;       // C-wave (sensory)

        public float Step(float dt, float sensory)
        {
            t += dt;

            // A-wave: identity + prediction carrier
            float A = CarrierOscillator(AlphaBetaFreq, t);

            // B-wave: routing + analog computation
            float B = RoutingOscillator(BetaGammaFreq, t);

            // C-wave: sensory binding
            float C = SensoryOscillator(GammaFreq, sensory, t);

            // D-wave: delta sidebands (restoration)
            float D = DeltaSideband(A, B);

            // Z-wave: harmonic meta-layer
            float Z = HarmonicInterference(A, B, C, D);

            // Combined consciousness state
            return A * (1 + B + C) + D + Z;
        }

        private float CarrierOscillator(float f, float t)
        {
            return MathF.Cos(2 * MathF.PI * f * t);
        }

        private float RoutingOscillator(float f, float t)
        {
            return MathF.Sin(2 * MathF.PI * f * t);
        }

        private float SensoryOscillator(float f, float sensory, float t)
        {
            return sensory * MathF.Cos(2 * MathF.PI * f * t);
        }

        private float DeltaSideband(float A, float B)
        {
            return 0.5f * (A - B);
        }

        private float HarmonicInterference(float A, float B, float C, float D)
        {
            return (A * B * C) - (0.1f * D);
        }
    }
}

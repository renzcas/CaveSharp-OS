using CaveSharp.Core;
using CaveSharp.Core.MindWaves;

using CaveSharp.Factions;


namespace CaveSharp.Factions
{
    public class MindWavesFactionMind
    {
        private readonly Kernel kernel;

        public MindWavesFactionMind(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public void ApplyTo(IFaction faction)
        {
            float c = kernel.MindWaves.Consciousness;

            faction.GroupMind = c;

            // Faction-level behavior modulation
            if (c < -0.5f)
            {
                faction.State = "Dormant";        // D-wave
            }
            else if (c < 0.5f)
            {
                faction.State = "Stable";         // A-wave
            }
            else if (c < 1.5f)
            {
                faction.State = "Coordinating";   // B-wave
            }
            else if (c < 3.0f)
            {
                faction.State = "Mobilizing";     // C-wave
            }
            else
            {
                faction.State = "Unified";        // Z-wave (hive-mind)
            }
        }
    }
}

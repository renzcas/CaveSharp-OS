using CaveSharp.Core.MindWaves;

using CaveSharp.Personas;
using CaveSharp.Factions;



namespace CaveSharp.Core
{
    public class MindWavesPersonaBinding
    {
        private readonly Kernel kernel;

        public float Consciousness => kernel.MindWaves.Consciousness;

        public MindWavesPersonaBinding(Kernel kernel)
        {
            this.kernel = kernel;
        }

        // Attach consciousness to any persona-like object
        public void ApplyTo(IPersona persona)
        {
            persona.Consciousness = Consciousness;
        }

        // Attach consciousness to creatures
        public void ApplyTo(ICreature creature)
        {
            creature.MindState = Consciousness;
        }

        // Attach consciousness to factions
        public void ApplyTo(IFaction faction)
        {
            faction.GroupMind = Consciousness;
        }
    }
}

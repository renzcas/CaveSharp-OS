using CaveSharpOS.Systems.Mythic;
using CaveSharpOS.Personas;
using CaveSharpOS.Time;

namespace CaveSharpOS.Core
{
    /// <summary>
    /// Central wiring hub for all CaveSharp-OS subsystems.
    /// Every engine is instantiated and exposed here.
    /// KernelPulseLoop calls into this binder each tick.
    /// </summary>
    public class SystemBinder
    {
        // Mythic subsystem engines
        public ArchetypeEngine Archetypes { get; private set; }
        public CycleEngine Cycles { get; private set; }
        public SymbolEngine Symbols { get; private set; }
        public MythicLawEngine Laws { get; private set; }
        public MythicMemoryEngine Memory { get; private set; }
        public MythicNarrativeEngine Myth { get; private set; }

        // Persona subsystem engines
        public PersonaEngine Persona { get; private set; }
        public DialogueEngine Dialogue { get; private set; }

        // Time subsystem engine
        public EventEngine Events { get; private set; }

        public SystemBinder()
        {
            // Instantiate all engines
            Archetypes = new ArchetypeEngine();
            Cycles = new CycleEngine();
            Symbols = new SymbolEngine();
            Laws = new MythicLawEngine();
            Memory = new MythicMemoryEngine();
            Myth = new MythicNarrativeEngine();

            Persona = new PersonaEngine();
            Dialogue = new DialogueEngine();

            Events = new EventEngine();
        }
    }
}

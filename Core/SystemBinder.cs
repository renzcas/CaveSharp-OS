using System;
using CaveSharpOS.Systems.Mythic;
using CaveSharpOS.Systems.World;
using CaveSharpOS.Systems.Interaction;

namespace CaveSharpOS.Core
{
    public class SystemBinder
    {
        public MythEngine Myth { get; private set; }
        public ArchetypeEngine Archetypes { get; private set; }
        public MythCycleEngine Cycles { get; private set; }
        public SymbolicDynamicsEngine Symbols { get; private set; }
        public MythicLawEngine Laws { get; private set; }
        public MythicMemoryEngine Memory { get; private set; }
        public MythicNarrativeEngine Narrative { get; private set; }

        public EcologyEngine Ecology { get; private set; }
        public WeatherEngine Weather { get; private set; }
        public MapEngine Map { get; private set; }

        public PersonaEngine Persona { get; private set; }
        public DialogueEngine Dialogue { get; private set; }
        public FactionEngine Factions { get; private set; }
        public EventEngine Events { get; private set; }

        public void Initialize()
        {
            // Mythic subsystem
            Myth = new MythEngine();
            Archetypes = new ArchetypeEngine();
            Cycles = new MythCycleEngine();
            Symbols = new SymbolicDynamicsEngine();
            Laws = new MythicLawEngine();
            Memory = new MythicMemoryEngine();
            Narrative = new MythicNarrativeEngine();

            Narrative.Inject(Myth, Archetypes, Cycles, Symbols, Laws, Memory);

            // World subsystem
            Ecology = new EcologyEngine();
            Weather = new WeatherEngine();
            Map = new MapEngine();

            // Interaction subsystem
            Persona = new PersonaEngine();
            Dialogue = new DialogueEngine();
            Factions = new FactionEngine();
            Events = new EventEngine();
        }
    }
}

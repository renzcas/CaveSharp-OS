using System;

namespace CaveSharpOS.Core
{
    public class KernelPulseLoop
    {
        private readonly SystemBinder binder;
        private bool running;

        public KernelPulseLoop(SystemBinder binder)
        {
            this.binder = binder;
        }

        public void Start()
        {
            running = true;

            while (running)
            {
                float dt = 0.016f; // ~60 FPS pulse

                // Mythic subsystem
                binder.Myth.Update(dt);
                binder.Archetypes.Update(dt);
                binder.Cycles.Update(dt);
                binder.Symbols.Update(dt);
                binder.Laws.Update(dt);
                binder.Memory.Update(dt);
                binder.Narrative.Update(dt);

                // World subsystem
                binder.Ecology.Update(dt);
                binder.Weather.Update(dt);
                binder.Map.Update(dt);

                // Interaction subsystem
                binder.Persona.Update(dt);
                binder.Dialogue.Update(dt);
                binder.Factions.Update(dt);
                binder.Events.Update(dt);

                // TODO: Add Physics, Audio, Input, Agents, CyberLab
            }
        }

        public void Stop()
        {
            running = false;
        }
    }
}

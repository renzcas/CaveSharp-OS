using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.Time.Demo
{
    public class CaveChamber
    {
        public WorldState World { get; } = new WorldState();
        public AgentState Agents { get; } = new AgentState();
        public EventState Events { get; } = new EventState();

        public CaveChamber()
        {
            // Three demo entities drifting in the cave chamber
            World.Entities.Add(new Entity
            {
                Position = new Vector3(0, 0, 0),
                Velocity = new Vector3(0.5f, 0, 0),
                Tags = new List<string> { "HERO_ENTITY" }
            });
            World.Entities.Add(new Entity { Position = new Vector3(-1, 0, 0), Velocity = new Vector3(0.2f, 0, 0) });
            World.Entities.Add(new Entity { Position = new Vector3(1, 0, 0), Velocity = new Vector3(-0.3f, 0, 0) });

            // One demo agent
            var watcher = new DemoAgent("Watcher");
            watcher.Actions.Add(new AgentAction { Description = "Observe the chamber" });
            Agents.Agents.Add(watcher);
        }
    }
}

using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.Time.Demo
{
    public class DemoAgent : Agent
    {
        public DemoAgent(string name)
        {
            Name = name;
        }

        public void React(EventState events)
        {
            Actions.Clear();

            foreach (var ev in events.SymbolicEvents)
            {
                Actions.Add(new AgentAction
                {
                    Description = $"[{Name}] reacts to {ev.Meaning}",
                    Weight = ev.Priority
                });
            }
        }
    }
}

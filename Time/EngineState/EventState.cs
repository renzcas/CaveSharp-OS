namespace CaveSharpOS.Time.EngineState;

public class EventState
{
    public List<string> Events { get; set; } = new();
    public List<string> CurrentEvents { get; set; } = new();
    public Dictionary<string, float> ExternalMetrics { get; set; } = new();
    public List<SymbolicEvent> SymbolicEvents { get; set; } = new();
}

public class SymbolicEvent
{
    public string Meaning { get; set; } = string.Empty;
    public float Priority { get; set; }
}

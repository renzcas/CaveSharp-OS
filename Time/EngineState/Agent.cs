namespace CaveSharpOS.Time.EngineState;

public class Agent
{
    public string Name { get; set; } = string.Empty;
    public List<AgentAction> Actions { get; set; } = new();
    public float AttentionWeight { get; set; }
    public float Fear { get; set; }
    public float Panic { get; set; }
    public float Trust { get; set; } = 1f;
    public float Aggression { get; set; }
    public float Morale { get; set; } = 0.65f;
    public float ExternalFearPressure { get; set; }
    public float ExternalPanicPressure { get; set; }
    public float ExternalTrustPressure { get; set; }
}

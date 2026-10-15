namespace CaveSharpOS.Time.EngineState;

public class Entity
{
    public Vector3 Position { get; set; }
    public Vector3 Velocity { get; set; }
    public List<string> Tags { get; set; } = new();
}

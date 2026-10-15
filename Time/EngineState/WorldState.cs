using System.Collections.Generic;
using System.Linq;

namespace CaveSharpOS.Time.EngineState;

public class WorldState
{
    public List<Entity> Entities { get; set; } = new();
}

public static class WorldStateExtensions
{
    public static Dictionary<string, object> ToDictionary(this WorldState world)
    {
        ArgumentNullException.ThrowIfNull(world);

        var entities = world.Entities
            .Select(entity => new
            {
                Position = new { entity.Position.X, entity.Position.Y, entity.Position.Z },
                Velocity = new { entity.Velocity.X, entity.Velocity.Y, entity.Velocity.Z }
            })
            .ToArray();

        return new Dictionary<string, object>
        {
            ["entityCount"] = entities.Length,
            ["entities"] = entities
        };
    }
}

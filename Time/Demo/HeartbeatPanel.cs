using System;
using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.Time.Demo;

public static class HeartbeatPanel
{
    public static void Update(WorldState world, EventState events, AgentState agents)
    {
        Console.WriteLine("=== CaveSharp-OS Heartbeat ===");

        Console.WriteLine("Entities:");
        foreach (var e in world.Entities)
            Console.WriteLine($"  Pos: ({e.Position.X:F2}, {e.Position.Y:F2}, {e.Position.Z:F2})");

        Console.WriteLine("Symbolic Events:");
        foreach (var ev in events.SymbolicEvents)
            Console.WriteLine($"  {ev.Meaning} [priority {ev.Priority:F2}]");

        Console.WriteLine("Agents:");
        foreach (var a in agents.Agents)
            Console.WriteLine($"  {a.Name} attention {a.AttentionWeight:F2}, actions {a.Actions.Count}");
    }
}

using System;
using CaveSharpOS.Time.EngineState;
using CaveSharpOS.Systems;   // For StateLogger
using CaveSharpOS.Triggers;  // For TriggerEngine
using CaveSharpOS.Emotions;

namespace CaveSharpOS.Time
{
    /// <summary>
    /// The tri-core pulse engine for CaveSharp-OS.
    /// Physics → Symbolic → Attention
    /// </summary>
    public static class TimeEngine
    {
        // ------------------------------------------------------------
        // INTERNAL STATE
        // ------------------------------------------------------------
        private static long _tick = 0;
        private static readonly StateLogger _logger = new StateLogger("logs/world_state.jsonl");
        private static readonly TrendAnalyzer _trendAnalyzer = new();

        // ------------------------------------------------------------
        // PUBLIC TICK WRAPPER
        // ------------------------------------------------------------
        public static TrendReport Tick(
            WorldState world,
            EventState events,
            AgentState agents,
            Action<WorldState, EventState, AgentState>? beforeEmotionUpdate = null)
        {
            ArgumentNullException.ThrowIfNull(world);
            ArgumentNullException.ThrowIfNull(events);
            ArgumentNullException.ThrowIfNull(agents);

            _tick++;
            events.CurrentEvents.Clear();

            // 1. Run the tri-core pulse
            Pulse(world, events, agents);

            // 2. Apply optional external inputs before evolving emotions.
            beforeEmotionUpdate?.Invoke(world, events, agents);
            EmotionalCurveEngine.Apply(world, events, agents);
            TriggerEngine.Check(world, events, agents, _tick);

            // 3. Analyze rolling dynamics and log the post-update state.
            var trends = _trendAnalyzer.Analyze(world, events, agents, _tick);
            var snapshot = world.ToDictionary();
            snapshot["events"] = events.Events;
            snapshot["currentEvents"] = events.CurrentEvents;
            snapshot["externalMetrics"] = events.ExternalMetrics;
            snapshot["symbolicEvents"] = events.SymbolicEvents;
            snapshot["agents"] = agents.Agents;
            snapshot["trends"] = trends;
            _logger.Log(_tick, snapshot);
            return trends;
        }

        // ------------------------------------------------------------
        // TRI-CORE PULSE
        // ------------------------------------------------------------
        public static void Pulse(WorldState world, EventState events, AgentState agents)
        {
            PhysicsPulse(world);
            SymbolicPulse(world, events);
            AttentionPulse(events, agents);
        }

        // ------------------------------------------------------------
        // PHYSICS PULSE
        // ------------------------------------------------------------
        private static void PhysicsPulse(WorldState world)
        {
            foreach (var e in world.Entities)
            {
                e.Position += e.Velocity;
            }
        }

        // ------------------------------------------------------------
        // SYMBOLIC PULSE
        // ------------------------------------------------------------
        private static void SymbolicPulse(WorldState world, EventState events)
        {
            events.SymbolicEvents.Clear();

            foreach (var e in world.Entities)
            {
                float energy =
                    Math.Abs(e.Velocity.X) +
                    Math.Abs(e.Velocity.Y) +
                    Math.Abs(e.Velocity.Z);

                events.SymbolicEvents.Add(new SymbolicEvent
                {
                    Meaning = $"Entity energy {energy:F2}",
                    Priority = energy
                });
            }
        }

        // ------------------------------------------------------------
        // ATTENTION PULSE
        // ------------------------------------------------------------
        private static void AttentionPulse(EventState events, AgentState agents)
        {
            float totalPriority = 0f;

            foreach (var ev in events.SymbolicEvents)
                totalPriority += ev.Priority;

            foreach (var agent in agents.Agents)
            {
                agent.AttentionWeight =
                    totalPriority > 0
                    ? agent.Actions.Count / totalPriority
                    : 0f;
            }
        }
    }
}

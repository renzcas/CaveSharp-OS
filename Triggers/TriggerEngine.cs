using System.Collections.Generic;
using System;
using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.Triggers
{
    public static class TriggerEngine
    {
        private const int Window = 5;
        private const int MinimumHistory = 3;
        private const float PanicThreshold = 0.7f;
        private const float EnergyThreshold = 5.0f;
        private const float TrustThreshold = 0.3f;

        private static readonly Queue<float> PanicHistory = new();
        private static readonly Queue<float> EnergyHistory = new();
        private static readonly Queue<float> TrustHistory = new();
        private static bool _panicCascadeActive;
        private static bool _energySurgeActive;
        private static bool _misinformationWaveActive;

        public static void Check(WorldState world, EventState events, AgentState agents, long tick)
        {
            ArgumentNullException.ThrowIfNull(world);
            ArgumentNullException.ThrowIfNull(events);
            ArgumentNullException.ThrowIfNull(agents);

            var avgEnergy = ComputeAverageEnergy(world);
            var avgPanic = ComputeAveragePanic(agents);
            var avgTrust = ComputeAverageTrust(agents);

            UpdateHistory(PanicHistory, avgPanic);
            UpdateHistory(EnergyHistory, avgEnergy);
            UpdateHistory(TrustHistory, avgTrust);

            CheckPanicCascade(avgPanic, events, tick);
            CheckEnergySurge(avgEnergy, events, tick);
            CheckMisinformationWave(avgTrust, events, tick);
        }

        public static void Reset()
        {
            PanicHistory.Clear();
            EnergyHistory.Clear();
            TrustHistory.Clear();
            _panicCascadeActive = false;
            _energySurgeActive = false;
            _misinformationWaveActive = false;
        }

        private static void UpdateHistory(Queue<float> history, float value)
        {
            history.Enqueue(value);
            if (history.Count > Window)
                history.Dequeue();
        }

        private static float Average(Queue<float> values)
        {
            var total = 0f;
            foreach (var value in values)
                total += value;

            return values.Count == 0 ? 0f : total / values.Count;
        }

        private static float ComputeAverageEnergy(WorldState world)
        {
            var total = 0f;
            foreach (var entity in world.Entities)
            {
                total += Math.Abs(entity.Velocity.X)
                    + Math.Abs(entity.Velocity.Y)
                    + Math.Abs(entity.Velocity.Z);
            }

            return world.Entities.Count == 0 ? 0f : total / world.Entities.Count;
        }

        private static float ComputeAveragePanic(AgentState agents)
        {
            var total = 0f;
            foreach (var agent in agents.Agents)
                total += agent.Panic;

            return agents.Agents.Count == 0 ? 0f : total / agents.Agents.Count;
        }

        private static float ComputeAverageTrust(AgentState agents)
        {
            var total = 0f;
            foreach (var agent in agents.Agents)
                total += agent.Trust;

            return agents.Agents.Count == 0 ? 1f : total / agents.Agents.Count;
        }

        private static void CheckPanicCascade(float avgPanic, EventState events, long tick)
        {
            var triggered = UpdateLatch(
                avgPanic >= PanicThreshold && PanicHistory.Count >= MinimumHistory && Average(PanicHistory) > 0.6f,
                avgPanic < 0.5f,
                ref _panicCascadeActive);

            if (triggered)
            {
                AddEvent(events, "PANIC_CASCADE", 1.0f, tick);
            }
        }

        private static void CheckEnergySurge(float avgEnergy, EventState events, long tick)
        {
            var triggered = UpdateLatch(
                avgEnergy >= EnergyThreshold && EnergyHistory.Count >= MinimumHistory && Average(EnergyHistory) > 4.0f,
                avgEnergy < 4.0f,
                ref _energySurgeActive);

            if (triggered)
            {
                AddEvent(events, "ENERGY_SURGE", 0.9f, tick);
            }
        }

        private static void CheckMisinformationWave(float avgTrust, EventState events, long tick)
        {
            var triggered = UpdateLatch(
                avgTrust <= TrustThreshold && TrustHistory.Count >= MinimumHistory && Average(TrustHistory) < 0.4f,
                avgTrust > 0.4f,
                ref _misinformationWaveActive);

            if (triggered)
            {
                AddEvent(events, "MISINFORMATION_WAVE", 0.8f, tick);
            }
        }

        private static bool UpdateLatch(bool condition, bool resetCondition, ref bool active)
        {
            if (resetCondition)
                active = false;

            if (!condition || active)
                return false;

            active = true;
            return true;
        }

        private static void AddEvent(EventState events, string meaning, float priority, long tick)
        {
            var message = $"{meaning} at tick {tick}";
            events.SymbolicEvents.Add(new SymbolicEvent
            {
                Meaning = message,
                Priority = priority
            });

            events.Events.Add(message);
            events.CurrentEvents.Add(message);
        }
    }
}

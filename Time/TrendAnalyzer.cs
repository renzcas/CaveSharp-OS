using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.Time;

public sealed class TrendAnalyzer
{
    private const int HistoryWindow = 10;
    private const int MinimumSamplesForTrend = 3;
    private const float SignificantChange = 0.15f;
    private const float LowCohesionThreshold = 0.55f;

    private readonly Queue<TickMetrics> _history = new();

    public TrendReport Analyze(WorldState world, EventState events, AgentState agents, long tick)
    {
        ArgumentNullException.ThrowIfNull(world);
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(agents);

        var current = Measure(world, events, agents, tick);
        _history.Enqueue(current);
        if (_history.Count > HistoryWindow)
            _history.Dequeue();

        var baseline = _history.Peek();
        var hasTrend = _history.Count >= MinimumSamplesForTrend;
        var fearChange = current.AverageFear - baseline.AverageFear;
        var panicChange = current.AveragePanic - baseline.AveragePanic;
        var trustChange = current.AverageTrust - baseline.AverageTrust;
        var aggressionChange = current.AverageAggression - baseline.AverageAggression;
        var moraleChange = current.AverageMorale - baseline.AverageMorale;
        var energyChange = current.AverageEnergy - baseline.AverageEnergy;
        var symbolicPriorityChange = current.AverageSymbolicPriority - baseline.AverageSymbolicPriority;
        var cohesionChange = current.AgentCohesion - baseline.AgentCohesion;
        var heroInfluenceChange = current.HeroEntityRatio - baseline.HeroEntityRatio;

        var signals = new List<string>();
        if (hasTrend)
        {
            AddSignal(signals, fearChange >= SignificantChange, "FEAR_RISING");
            AddSignal(signals, panicChange >= SignificantChange, "PANIC_RISING");
            AddSignal(signals, trustChange <= -SignificantChange, "TRUST_DECLINING");
            AddSignal(signals, moraleChange <= -SignificantChange, "MORALE_DECLINING");
            AddSignal(signals, symbolicPriorityChange >= SignificantChange, "SYMBOLIC_RESONANCE_RISING");
            AddSignal(signals, energyChange <= -SignificantChange, "WORLD_MOTION_DECLINING");
            AddSignal(
                signals,
                current.AgentCohesion < LowCohesionThreshold
                    && cohesionChange <= -SignificantChange,
                "AGENT_COHESION_COLLAPSING");
            AddSignal(signals, heroInfluenceChange > 0f, "HERO_INFLUENCE_SPREADING");
            AddSignal(
                signals,
                panicChange >= SignificantChange
                    && aggressionChange >= SignificantChange
                    && trustChange <= -SignificantChange,
                "MULTI_TICK_INSTABILITY");
        }

        return new TrendReport(
            tick,
            _history.Count,
            current.AverageFear,
            current.AveragePanic,
            current.AverageTrust,
            current.AverageAggression,
            current.AverageMorale,
            current.AverageEnergy,
            current.AverageSymbolicPriority,
            current.AgentCohesion,
            current.HeroEntityRatio,
            fearChange,
            panicChange,
            trustChange,
            aggressionChange,
            moraleChange,
            energyChange,
            symbolicPriorityChange,
            cohesionChange,
            heroInfluenceChange,
            signals);
    }

    public void Reset() => _history.Clear();

    private static TickMetrics Measure(WorldState world, EventState events, AgentState agents, long tick)
    {
        var fear = 0f;
        var panic = 0f;
        var trust = 0f;
        var aggression = 0f;
        var morale = 0f;
        var panicSquares = 0f;
        var trustSquares = 0f;
        var aggressionSquares = 0f;

        foreach (var agent in agents.Agents)
        {
            fear += Clamp01(agent.Fear);
            panic += Clamp01(agent.Panic);
            trust += Clamp01(agent.Trust);
            aggression += Clamp01(agent.Aggression);
            morale += Clamp01(agent.Morale);
            panicSquares += Square(Clamp01(agent.Panic));
            trustSquares += Square(Clamp01(agent.Trust));
            aggressionSquares += Square(Clamp01(agent.Aggression));
        }

        var agentCount = agents.Agents.Count;
        var averagePanic = Mean(panic, agentCount);
        var averageTrust = Mean(trust, agentCount);
        var averageAggression = Mean(aggression, agentCount);
        var dispersion = agentCount == 0
            ? 0f
            : MathF.Sqrt(Math.Max(
                0f,
                (
                    Variance(panicSquares, averagePanic, agentCount)
                    + Variance(trustSquares, averageTrust, agentCount)
                    + Variance(aggressionSquares, averageAggression, agentCount)
                ) / 3f));

        var totalEnergy = 0f;
        var heroCount = 0;
        foreach (var entity in world.Entities)
        {
            totalEnergy += Math.Abs(entity.Velocity.X)
                + Math.Abs(entity.Velocity.Y)
                + Math.Abs(entity.Velocity.Z);
            if (entity.Tags.Contains("HERO_ENTITY", StringComparer.Ordinal))
                heroCount++;
        }

        var totalSymbolicPriority = 0f;
        foreach (var symbolicEvent in events.SymbolicEvents)
            totalSymbolicPriority += symbolicEvent.Priority;

        return new TickMetrics(
            tick,
            Mean(fear, agentCount),
            averagePanic,
            averageTrust,
            averageAggression,
            Mean(morale, agentCount),
            Mean(totalEnergy, world.Entities.Count),
            Mean(totalSymbolicPriority, events.SymbolicEvents.Count),
            agentCount == 0 ? 1f : Clamp01(1f - dispersion),
            world.Entities.Count == 0 ? 0f : (float)heroCount / world.Entities.Count);
    }

    private static float Variance(float sumOfSquares, float mean, int count) =>
        count == 0 ? 0f : Math.Max(0f, sumOfSquares / count - Square(mean));

    private static float Mean(float total, int count) => count == 0 ? 0f : total / count;

    private static float Square(float value) => value * value;

    private static float Clamp01(float value) => Math.Clamp(value, 0f, 1f);

    private static void AddSignal(List<string> signals, bool condition, string signal)
    {
        if (condition)
            signals.Add(signal);
    }

    private sealed record TickMetrics(
        long Tick,
        float AverageFear,
        float AveragePanic,
        float AverageTrust,
        float AverageAggression,
        float AverageMorale,
        float AverageEnergy,
        float AverageSymbolicPriority,
        float AgentCohesion,
        float HeroEntityRatio);
}

public sealed record TrendReport(
    long Tick,
    int Samples,
    float AverageFear,
    float AveragePanic,
    float AverageTrust,
    float AverageAggression,
    float AverageMorale,
    float AverageEnergy,
    float AverageSymbolicPriority,
    float AgentCohesion,
    float HeroEntityRatio,
    float FearChange,
    float PanicChange,
    float TrustChange,
    float AggressionChange,
    float MoraleChange,
    float EnergyChange,
    float SymbolicPriorityChange,
    float CohesionChange,
    float HeroInfluenceChange,
    IReadOnlyList<string> Signals);

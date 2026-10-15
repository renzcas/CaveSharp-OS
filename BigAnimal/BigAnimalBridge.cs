using CaveSharpOS.Time.EngineState;
using CaveSharpOS.Time;

namespace CaveSharpOS.BigAnimal;

public sealed class BigAnimalBridge
{
    public void ApplyInputs(WorldHeart heart, EventState events, AgentState agents)
    {
        ArgumentNullException.ThrowIfNull(heart);
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(agents);
        heart.Validate();

        events.ExternalMetrics.Clear();
        events.ExternalMetrics[nameof(heart.Stability)] = heart.Stability;
        events.ExternalMetrics[nameof(heart.ConflictRisk)] = heart.ConflictRisk;
        events.ExternalMetrics[nameof(heart.EnergyPulse)] = heart.EnergyPulse;
        events.ExternalMetrics[nameof(heart.IndustrialStress)] = heart.IndustrialStress;
        events.ExternalMetrics[nameof(heart.ClimateStress)] = heart.ClimateStress;
        events.ExternalMetrics[nameof(heart.SocialCohesion)] = heart.SocialCohesion;
        events.ExternalMetrics[nameof(heart.MisinformationPressure)] = heart.MisinformationPressure;

        var fearPressure = Math.Max(heart.ConflictRisk, 1f - heart.Stability);
        var panicPressure = Math.Max(heart.ConflictRisk, heart.ClimateStress);
        var trustPressure = heart.MisinformationPressure;

        foreach (var agent in agents.Agents)
        {
            agent.ExternalFearPressure = fearPressure;
            agent.ExternalPanicPressure = panicPressure;
            agent.ExternalTrustPressure = trustPressure;
        }

        AddMetric(events, "BIG_ANIMAL_CONFLICT_RISK", heart.ConflictRisk);
        AddMetric(events, "BIG_ANIMAL_ENERGY_PULSE", heart.EnergyPulse);
        AddMetric(events, "BIG_ANIMAL_INDUSTRIAL_STRESS", heart.IndustrialStress);
        AddMetric(events, "BIG_ANIMAL_CLIMATE_STRESS", heart.ClimateStress);
        AddMetric(events, "BIG_ANIMAL_SOCIAL_COHESION", heart.SocialCohesion);
        AddMetric(events, "BIG_ANIMAL_MISINFORMATION_PRESSURE", heart.MisinformationPressure);
        AddMetric(events, "BIG_ANIMAL_STABILITY", heart.Stability);

        if (heart.MisinformationPressure >= 0.7f)
            AddSignal(events, "MISINFORMATION_WAVE", heart.MisinformationPressure);

        if (heart.ConflictRisk >= 0.7f || heart.SocialCohesion <= 0.3f)
            AddSignal(events, "PANIC_CASCADE", Math.Max(heart.ConflictRisk, 1f - heart.SocialCohesion));

        if (heart.EnergyPulse >= 0.7f || heart.IndustrialStress >= 0.7f)
            AddSignal(events, "ENERGY_SURGE", Math.Max(heart.EnergyPulse, heart.IndustrialStress));
    }

    public BigAnimalFeedback CreateFeedback(TrendReport trends)
    {
        ArgumentNullException.ThrowIfNull(trends);

        return new BigAnimalFeedback(
            trends.Tick,
            ToFeedbackDelta(trends.PanicChange),
            ToFeedbackDelta(-trends.TrustChange),
            ToFeedbackDelta(-trends.CohesionChange),
            ToFeedbackDelta(trends.EnergyChange),
            trends.Signals);
    }

    private static void AddMetric(EventState events, string name, float value)
    {
        events.SymbolicEvents.Add(new SymbolicEvent
        {
            Meaning = name,
            Priority = value
        });
    }

    private static void AddSignal(EventState events, string name, float intensity)
    {
        var message = $"{name} intensity={intensity:F2}";
        events.CurrentEvents.Add(message);
        events.Events.Add(message);
        events.SymbolicEvents.Add(new SymbolicEvent
        {
            Meaning = name,
            Priority = intensity
        });
    }

    private static float ToFeedbackDelta(float change) =>
        Math.Clamp(change, -0.1f, 0.1f);
}

public sealed record BigAnimalFeedback(
    long Tick,
    float ConflictRiskDelta,
    float MisinformationPressureDelta,
    float SocialCohesionDelta,
    float EnergyPulseDelta,
    IReadOnlyList<string> TrendSignals);

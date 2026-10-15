using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.Emotions;

public static class EmotionalCurveEngine
{
    private const float BaseFearGain = 0.02f;
    private const float PanicFromFearGain = 0.04f;
    private const float TrustDecayFromMisinfo = 0.03f;
    private const float AggressionFromPanicGain = 0.05f;
    private const float MoraleFromHeroGain = 0.03f;

    private const float FearDecay = 0.01f;
    private const float PanicDecay = 0.015f;
    private const float TrustRecovery = 0.01f;
    private const float AggressionDecay = 0.02f;
    private const float MoraleDecay = 0.01f;

    public static void Apply(WorldState world, EventState events, AgentState agents)
    {
        ArgumentNullException.ThrowIfNull(world);
        ArgumentNullException.ThrowIfNull(events);
        ArgumentNullException.ThrowIfNull(agents);

        var hasHero = HasHero(world);
        var hasMisinfoWave = HasEvent(events, "MISINFO_WAVE", "MISINFORMATION_WAVE");
        var hasPanicCascade = HasEvent(events, "PANIC_CASCADE");
        var hasEnergySurge = HasEvent(events, "ENERGY_SURGE");
        var averageEnergy = ComputeAverageEnergy(world);

        foreach (var agent in agents.Agents)
        {
            var fearDelta = averageEnergy > 3f || hasEnergySurge
                ? BaseFearGain * (averageEnergy / 3f)
                : 0f;
            fearDelta += agent.ExternalFearPressure * BaseFearGain;
            if (hasPanicCascade)
                fearDelta += BaseFearGain * 2f;

            if (hasHero)
                fearDelta -= BaseFearGain;

            agent.Fear = Clamp01(agent.Fear + fearDelta - FearDecay);

            var panicDelta = PanicFromFearGain * agent.Fear;
            if (hasMisinfoWave)
                panicDelta += PanicFromFearGain * 1.5f;
            if (hasPanicCascade)
                panicDelta += PanicFromFearGain;
            panicDelta += agent.ExternalPanicPressure * PanicFromFearGain;
            if (hasHero)
                panicDelta -= PanicFromFearGain;

            agent.Panic = Clamp01(agent.Panic + panicDelta - PanicDecay);

            if (hasMisinfoWave)
                agent.Trust -= TrustDecayFromMisinfo;
            else
                agent.Trust += TrustRecovery;
            agent.Trust -= agent.ExternalTrustPressure * TrustDecayFromMisinfo;
            if (hasHero)
                agent.Trust += TrustRecovery;
            agent.Trust = Clamp01(agent.Trust);

            var aggressionDelta = agent.Panic > 0.6f
                ? AggressionFromPanicGain * agent.Panic
                : 0f;
            agent.Aggression = Clamp01(agent.Aggression + aggressionDelta - AggressionDecay);

            var moraleDelta = hasHero ? MoraleFromHeroGain : 0f;
            moraleDelta -= MoraleDecay + agent.Panic * MoraleDecay;
            agent.Morale = Clamp01(agent.Morale + moraleDelta);
        }
    }

    private static float ComputeAverageEnergy(WorldState world)
    {
        if (world.Entities.Count == 0)
            return 0f;

        var totalEnergy = 0f;
        foreach (var entity in world.Entities)
        {
            totalEnergy += Math.Abs(entity.Velocity.X)
                + Math.Abs(entity.Velocity.Y)
                + Math.Abs(entity.Velocity.Z);
        }

        return totalEnergy / world.Entities.Count;
    }

    private static bool HasEvent(EventState events, params string[] eventNames)
    {
        foreach (var symbolicEvent in events.SymbolicEvents)
        {
            foreach (var eventName in eventNames)
            {
                if (symbolicEvent.Meaning.Contains(eventName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }

        foreach (var simulationEvent in events.CurrentEvents)
        {
            foreach (var eventName in eventNames)
            {
                if (simulationEvent.Contains(eventName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }

        return false;
    }

    private static bool HasHero(WorldState world)
    {
        foreach (var entity in world.Entities)
        {
            if (entity.Tags.Contains("HERO_ENTITY", StringComparer.Ordinal))
                return true;
        }

        return false;
    }

    private static float Clamp01(float value) => Math.Clamp(value, 0f, 1f);
}

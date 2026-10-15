using CaveSharpOS.Time;
using CaveSharpOS.Time.Demo;
using CaveSharpOS.Time.EngineState;

namespace CaveSharpOS.BigAnimal;

public sealed class BigAnimalSimulationDriver
{
    private readonly BigAnimalBridge _bridge;

    public BigAnimalSimulationDriver(BigAnimalBridge? bridge = null)
    {
        _bridge = bridge ?? new BigAnimalBridge();
    }

    public SimulationTickResult Tick(
        WorldHeart heart,
        WorldState world,
        EventState events,
        AgentState agents)
    {
        ArgumentNullException.ThrowIfNull(heart);
        var trends = TimeEngine.Tick(world, events, agents, (tickWorld, tickEvents, tickAgents) =>
            _bridge.ApplyInputs(heart, tickEvents, tickAgents));
        var feedback = _bridge.CreateFeedback(trends);

        return new SimulationTickResult(trends, feedback);
    }

    public static void RunDemo(int tickCount, TimeSpan tickDelay)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(tickCount);
        ArgumentOutOfRangeException.ThrowIfLessThan(tickDelay, TimeSpan.Zero);

        var chamber = new CaveChamber();
        var heart = new WorldHeart
        {
            Stability = 0.72f,
            ConflictRisk = 0.35f,
            EnergyPulse = 0.4f,
            IndustrialStress = 0.3f,
            ClimateStress = 0.2f,
            SocialCohesion = 0.8f,
            MisinformationPressure = 0.25f
        };
        var driver = new BigAnimalSimulationDriver();

        for (var tick = 0; tick < tickCount; tick++)
        {
            var result = driver.Tick(heart, chamber.World, chamber.Events, chamber.Agents);
            Console.WriteLine(
                $"[BIG_ANIMAL bridge] Tick={result.Trends.Tick} "
                + $"panic={result.Trends.AveragePanic:F2} "
                + $"conflict-risk Δ={result.Feedback.ConflictRiskDelta:+0.00;-0.00;0.00}");
            HeartbeatPanel.Update(chamber.World, chamber.Events, chamber.Agents);

            if (tickDelay > TimeSpan.Zero)
                Thread.Sleep(tickDelay);
        }
    }
}

public sealed record SimulationTickResult(TrendReport Trends, BigAnimalFeedback Feedback);

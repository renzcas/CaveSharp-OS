using System;
using CaveSharp.Core;
using CaveSharp.Core.MindWaves;
using CaveSharp.Environment;
using CaveSharp.Portals;
using CaveSharp.Creatures;
using CaveSharp.Realm;

class Program
{
    static void Main()
    {
        // Initialize Kernel
        Kernel kernel = new Kernel();

        // Initialize subsystems
        MindWavesDebugger debug = new MindWavesDebugger(kernel);
        MindWavesPortalActivation portal = new MindWavesPortalActivation(kernel);
        MindWavesWorldSensors sensors = new MindWavesWorldSensors();
        MindWavesCreatureBehaviors creatureBehaviors = new MindWavesCreatureBehaviors(kernel);
        MindWavesRealmHarmonics harmonics = new MindWavesRealmHarmonics(kernel);

        // Fake world + creature for testing
        World world = new World
        {
            NoiseLevel = 0.4f,
            LightLevel = 0.6f,
            MagicFlux = 0.3f,
            Temperature = 0.5f,
            BaseFlux = 0.2f,
            MaxFlux = 1.0f
        };

        TestCreature creature = new TestCreature();
        RealmRegion region = new RealmRegion { ResonanceFactor = 1.2f };

        // Run 10 ticks
        for (int i = 0; i < 10; i++)
        {
            float dt = 0.016f; // 60 FPS
            float sensory = sensors.ComputeSensoryField(world);

            kernel.Tick(dt, sensory);

            creatureBehaviors.Update(creature);
            harmonics.ApplyTo(region);

            debug.Print();

            Console.WriteLine($"Portal: {portal.PortalState()}");
            Console.WriteLine($"Creature Action: {creature.Action}");
            Console.WriteLine($"Region Ambience: {region.Ambience}");
            Console.WriteLine("----");
        }
    }
}

// Simple test creature
public class TestCreature : ICreature
{
    public float MindState { get; set; }
    public string Behavior { get; set; }
    public string Action { get; set; }
    public float Alertness { get; set; }
}

// Simple test region
public class RealmRegion
{
    public float ResonanceFactor { get; set; }
    public float HarmonicLevel { get; set; }
    public string Ambience { get; set; }
}

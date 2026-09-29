using System;

namespace CaveSharp.Realm
{
    public class NonLinearJunctionDetector
    {
        public int AnomalyLevel { get; private set; } = 0;
        public bool JunctionOpen { get; private set; } = false;

        public void Tick()
        {
            int roll = Random.Shared.Next(0, 100);

            // Small fluctuations
            if (roll < 50)
            {
                AnomalyLevel += Random.Shared.Next(0, 2);
            }
            // Medium spikes
            else if (roll < 80)
            {
                AnomalyLevel += Random.Shared.Next(1, 4);
            }
            // Rare major distortion
            else
            {
                AnomalyLevel += Random.Shared.Next(3, 8);
                Console.WriteLine("[Realm Junction] *** MAJOR distortion detected ***");
            }

            // Clamp anomaly level
            if (AnomalyLevel > 100)
                AnomalyLevel = 100;

            // Junction opens when anomaly level crosses threshold
            if (!JunctionOpen && AnomalyLevel >= 40)
            {
                JunctionOpen = true;
                Console.WriteLine("[Realm Junction] >>> A dimensional junction has opened.");
            }

            // Junction closes if anomaly level drops
            if (JunctionOpen && AnomalyLevel < 20)
            {
                JunctionOpen = false;
                Console.WriteLine("[Realm Junction] <<< Junction has stabilized and closed.");
            }

            Console.WriteLine($"[Realm Junction] AnomalyLevel={AnomalyLevel}, Open={JunctionOpen}");
        }
    }
}

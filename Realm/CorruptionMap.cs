using System;

namespace CaveSharp.Realm
{
    public class CorruptionMap
    {
        public int SpreadIntensity { get; private set; } = 1;

        public void Tick()
        {
            int roll = Random.Shared.Next(0, 100);

            if (roll < 30)
            {
                SpreadIntensity++;
                Console.WriteLine($"[Realm Corruption] Spread intensity increased to {SpreadIntensity}");
            }
            else if (roll > 95)
            {
                SpreadIntensity = Math.Max(1, SpreadIntensity - 1);
                Console.WriteLine($"[Realm Corruption] Spread intensity decreased to {SpreadIntensity}");
            }
        }
    }
}

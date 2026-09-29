using System;

namespace CaveSharp.Realm
{
    public class HealingMap
    {
        public int HealingStrength { get; private set; } = 1;

        public void Tick()
        {
            int roll = Random.Shared.Next(0, 100);

            if (roll < 20)
            {
                HealingStrength++;
                Console.WriteLine($"[Realm Healing] Healing strength increased to {HealingStrength}");
            }
            else if (roll > 95)
            {
                HealingStrength = Math.Max(1, HealingStrength - 1);
                Console.WriteLine($"[Realm Healing] Healing strength decreased to {HealingStrength}");
            }
        }
    }
}

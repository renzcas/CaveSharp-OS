using System;

namespace CaveSharp.CTA
{
    public class CTAEventEngine
    {
        public void Tick()
        {
            int roll = Random.Shared.Next(0, 100);

            if (roll < 10)
            {
                Console.WriteLine("[CTA Event] Minor corruption pulse detected.");
            }
            else if (roll < 3)
            {
                Console.WriteLine("[CTA Event] MAJOR outbreak triggered!");
            }
        }
    }
}

using System;

namespace CaveSharp.Realm
{
    public class SpatialRiftEngine
    {
        private readonly NonLinearJunctionDetector _junctions;

        public SpatialRiftEngine(NonLinearJunctionDetector junctions)
        {
            _junctions = junctions;
        }

        public void Tick()
        {
            if (!_junctions.JunctionOpen)
                return;

            int roll = Random.Shared.Next(0, 100);

            if (roll < 50)
            {
                Console.WriteLine("[Realm Rift] Minor spatial tear flickers.");
            }
            else if (roll < 85)
            {
                Console.WriteLine("[Realm Rift] Rift pulse destabilizes nearby terrain.");
            }
            else
            {
                Console.WriteLine("[Realm Rift] *** MAJOR RIFT EVENT: Geometry collapses inward ***");
            }
        }
    }
}

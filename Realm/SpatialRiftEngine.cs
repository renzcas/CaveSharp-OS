using System;

namespace CaveSharp.Realm
{
    public class SpatialRiftEngine
    {
        private readonly NonLinearJunctionDetector _junctions;
        private int _stability;

        public SpatialRiftEngine(NonLinearJunctionDetector junctions)
        {
            _junctions = junctions;
            _stability = 100; // fully stable at start
        }

        /// <summary>
        /// Called by UpdateLoop.cs
        /// </summary>
        public void Update()
        {
            if (_junctions.JunctionOpen)
            {
                // Rift destabilizes when junction is open
                _stability -= 5;
                if (_stability < 0) _stability = 0;

                Console.WriteLine($"[Realm Rift] Rift destabilizing. Stability={_stability}");
            }
            else
            {
                // Rift stabilizes when junction is closed
                _stability += 3;
                if (_stability > 100) _stability = 100;

                Console.WriteLine($"[Realm Rift] Rift stabilizing. Stability={_stability}");
            }
        }

        public int GetStability()
        {
            return _stability;
        }
    }
}

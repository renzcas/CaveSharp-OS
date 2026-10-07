using System;

namespace CaveSharp.Realm
{
    /// <summary>
    /// Tracks corruption intensity across the world.
    /// Provides sampling for creature senses and movement.
    /// </summary>
    public class CorruptionMap
    {
        // Global corruption pressure
        public float SpreadIntensity { get; set; } = 0f;

        // Simple grid model (optional future expansion)
        private readonly float[,] _grid = new float[32, 32];

        public void Spread()
        {
            // Basic corruption growth
            SpreadIntensity += 0.2f;
            if (SpreadIntensity > 100f)
                SpreadIntensity = 100f;
        }

        /// <summary>
        /// REQUIRED by MovementEngine and SensesEngine.
        /// Returns corruption intensity at a given world coordinate.
        /// </summary>
        public float Sample(float x, float y)
        {
            // Normalize coordinates into grid space
            int gx = (int)Math.Clamp(x, 0, 31);
            int gy = (int)Math.Clamp(y, 0, 31);

            // Combine global pressure + local grid
            float local = _grid[gx, gy];
            return (SpreadIntensity * 0.01f) + local;
        }

        public float[,] GetGrid()
        {
            return _grid;
        }

        public void IncreasePressure()
        {
            SpreadIntensity += 5f;
            if (SpreadIntensity > 100f)
                SpreadIntensity = 100f;
        }
    }
}

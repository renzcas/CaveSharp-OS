using System;

namespace CaveSharp.Realm
{
    public class CorruptionMap
    {
        // === Core corruption metrics ===
        public int SpreadIntensity { get; private set; } = 0;
        public float Pressure { get; private set; } = 0f;

        // === Corruption grid for heatmap ===
        private readonly float[,] _grid;
        private readonly int _width;
        private readonly int _height;

        private readonly Random _rng = new Random();

        public CorruptionMap(int width = 16, int height = 8)
        {
            _width = width;
            _height = height;
            _grid = new float[_width, _height];

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                    _grid[x, y] = 0f;
        }

        /// <summary>
        /// Called by UpdateLoop.cs
        /// </summary>
        public void Spread()
        {
            // SpreadIntensity rises slowly over time
            SpreadIntensity += 1;
            if (SpreadIntensity > 100)
                SpreadIntensity = 100;

            // Random corruption spike in the grid
            int x = _rng.Next(_width);
            int y = _rng.Next(_height);

            _grid[x, y] += Pressure * 0.3f;
            if (_grid[x, y] > 100f)
                _grid[x, y] = 100f;

            Console.WriteLine($"[Realm Corruption] Spread event. Intensity={SpreadIntensity}, Pressure={Pressure}");
        }

        /// <summary>
        /// Overseer calls this every tick.
        /// </summary>
        public void Decay(float dt)
        {
            // Pressure naturally decays
            Pressure -= dt * 0.1f;
            if (Pressure < 0) Pressure = 0;

            // Spread intensity decays slowly
            SpreadIntensity -= (int)(dt * 0.05f);
            if (SpreadIntensity < 0) SpreadIntensity = 0;

            // Grid corruption decays
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _grid[x, y] -= dt * 0.2f;
                    if (_grid[x, y] < 0) _grid[x, y] = 0;
                }
            }
        }

        /// <summary>
        /// Overseer increases corruption pressure when threat rises.
        /// </summary>
        public void IncreasePressure()
        {
            Pressure += 5f;
            SpreadIntensity += 2;

            // Randomly intensify grid corruption
            int x = _rng.Next(_width);
            int y = _rng.Next(_height);

            _grid[x, y] += Pressure * 0.5f;
            if (_grid[x, y] > 100f)
                _grid[x, y] = 100f;
        }

        /// <summary>
        /// Overseer sends this to RB-App cockpit.
        /// </summary>
        public float[,] GetGrid()
        {
            return _grid;
        }
    }
}

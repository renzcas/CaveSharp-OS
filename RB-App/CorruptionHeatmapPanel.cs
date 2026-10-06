using System;

namespace RB_App
{
    public class CorruptionHeatmapPanel
    {
        private int[,] _map = new int[5, 5];

        public void Update(int[,] map)
        {
            _map = map;
        }

        public void Render()
        {
            Console.WriteLine("=== Corruption Heatmap ===");

            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    int v = _map[y, x];
                    char c = v switch
                    {
                        < 20 => '.',
                        < 40 => '-',
                        < 60 => '+',
                        < 80 => '*',
                        _ => '#'
                    };

                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}

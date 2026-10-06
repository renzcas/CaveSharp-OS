using System;

namespace RB_App
{
    public class CreaturePanel
    {
        private int _count = 0;

        public void Update(int count)
        {
            _count = count;
        }

        public void Render()
        {
            Console.WriteLine("=== Creature Panel ===");
            Console.WriteLine($"Creatures Active: {_count}");
        }
    }
}

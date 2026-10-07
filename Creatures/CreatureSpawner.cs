using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Handles creature creation, random spawning, and controlled population growth.
    /// Overseer and UpdateLoop use this to introduce new life into the cave.
    /// </summary>
    public class CreatureSpawner
    {
        private readonly CreatureManager _manager;

        // Spawn tuning
        public int MaxCreatures { get; set; } = 25;
        public float SpawnInterval { get; set; } = 5f;

        private float _timer = 0f;

        private readonly string[] _names =
        {
            "Grotling", "Stoneback", "Murkling", "Hollowcrawler",
            "Fangmite", "Duststrider", "Glowbeast", "Cave Wisp"
        };

        public CreatureSpawner(CreatureManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Called by UpdateLoop.cs
        /// </summary>
        public void Tick(float dt)
        {
            _timer += dt;

            if (_timer >= SpawnInterval)
            {
                _timer = 0f;
                TrySpawn();
            }
        }

        private void TrySpawn()
        {
            var all = _manager.GetAll();
            if (all.Count >= MaxCreatures)
                return;

            string name = _names[Random.Shared.Next(0, _names.Length)];
            float x = Random.Shared.Next(-20, 21);
            float y = Random.Shared.Next(-20, 21);

            var creature = new Creature(name)
            {
                X = x,
                Y = y
            };

            _manager.Register(creature);

            Console.WriteLine($"[Spawner] Spawned {name} at ({x},{y})");
        }

        /// <summary>
        /// Manual spawn used by Overseer or CTA events.
        /// </summary>
        public Creature SpawnManual(string name, float x, float y)
        {
            var creature = new Creature(name)
            {
                X = x,
                Y = y
            };

            _manager.Register(creature);

            Console.WriteLine($"[Spawner] Manually spawned {name} at ({x},{y})");

            return creature;
        }
    }
}

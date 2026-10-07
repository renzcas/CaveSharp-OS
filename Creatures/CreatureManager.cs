using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Central registry and update controller for all creatures.
    /// Overseer, WebUI, Realm systems, and Factions all depend on this.
    /// </summary>
    public class CreatureManager
    {
        private readonly List<Creature> _creatures = new();

        public void Add(Creature creature)
        {
            _creatures.Add(creature);
            Console.WriteLine($"[CreatureManager] Added creature: {creature.Name}");
        }

        public IReadOnlyList<Creature> GetAll()
        {
            return _creatures;
        }

        /// <summary>
        /// Called by UpdateLoop.cs
        /// </summary>
        public void Tick(float dt)
        {
            foreach (var c in _creatures)
            {
                if (!c.IsAfraid && c.Hunger > 80)
                {
                    c.BecomeAfraid("Starvation stress");
                }

                c.Tick(dt);
            }
        }

        /// <summary>
        /// Overseer uses this to compute threat levels.
        /// </summary>
        public int CountLowHealth(float threshold = 40f)
        {
            int count = 0;
            foreach (var c in _creatures)
                if (c.Health < threshold)
                    count++;

            return count;
        }

        /// <summary>
        /// WebUI and Telemetry use this.
        /// </summary>
        public object GetSnapshot()
        {
            var list = new List<object>();

            foreach (var c in _creatures)
            {
                list.Add(new
                {
                    name = c.Name,
                    hp = c.Health,
                    aggression = c.Aggression,
                    hunger = c.Hunger,
                    corruption = c.CorruptionExposure,
                    afraid = c.IsAfraid,
                    x = c.X,
                    y = c.Y
                });
            }

            return list;
        }
    }
}

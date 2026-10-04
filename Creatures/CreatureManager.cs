using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    public class CreatureManager
    {
        private readonly List<Creature> _creatures = new();

        public void Register(Creature creature)
        {
            _creatures.Add(creature);
            Console.WriteLine($"[CreatureManager] Registered creature: {creature.Name}");
        }

        public IReadOnlyList<Creature> GetAll() => _creatures;

        /// <summary>
        /// Called every tick by Overseer.cs
        /// </summary>
        public void UpdateAll(float dt)
        {
            foreach (var c in _creatures)
            {
                c.Update(dt);
            }
        }

        /// <summary>
        /// Overseer uses this to send creature telemetry to RB-App cockpit.
        /// </summary>
        public List<CreatureSnapshot> GetSnapshot()
        {
            var list = new List<CreatureSnapshot>();

            foreach (var c in _creatures)
            {
                list.Add(new CreatureSnapshot
                {
                    Name = c.Name,
                    Health = c.Health,
                    Aggression = c.Aggression,
                    X = c.Position.X,
                    Y = c.Position.Y,
                    CorruptionExposure = c.CorruptionExposure
                });
            }

            return list;
        }
    }
}

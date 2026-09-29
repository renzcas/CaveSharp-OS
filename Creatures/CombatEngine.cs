using System;
using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    public class CreatureManager
    {
        private readonly List<Creature> _creatures = new();

        private readonly BehaviorEngine _behavior;
        private readonly MovementEngine _movement;
        private readonly SensesEngine _senses;

        public CreatureManager(
            BehaviorEngine behavior,
            MovementEngine movement,
            SensesEngine senses)
        {
            _behavior = behavior;
            _movement = movement;
            _senses = senses;
        }

        public void Register(Creature creature)
        {
            _creatures.Add(creature);
            Console.WriteLine($"[CreatureManager] Registered creature: {creature.Name}");
        }

        public void Tick()
        {
            foreach (var creature in _creatures)
            {
                // Biological update
                creature.Tick();

                // AI decision-making
                _behavior.Tick(creature);

                // Movement + spatial reaction
                _movement.Tick(creature);

                // Sensory perception
                _senses.Tick(creature, _creatures);
            }
        }
    }
}

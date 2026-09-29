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
        private readonly CombatEngine _combat;

        public CreatureManager(
            BehaviorEngine behavior,
            MovementEngine movement,
            SensesEngine senses,
            CombatEngine combat)
        {
            _behavior = behavior;
            _movement = movement;
            _senses = senses;
            _combat = combat;
        }

        public void Register(Creature creature)
        {
            _creatures.Add(creature);
            Console.WriteLine($"[CreatureManager] Registered creature: {creature.Name}");
        }

        public IReadOnlyList<Creature> GetAll() => _creatures;

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

                // Combat interactions
                _combat.Tick(creature, _creatures);
            }
        }
    }
}

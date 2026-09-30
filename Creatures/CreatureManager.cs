using System;
using System.Collections.Generic;
using CaveSharpOS.Systems.Combat;

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
                creature.Tick();
                _behavior.Tick(creature);
                _movement.Tick(creature);
                _senses.Tick(creature, _creatures);
                _combat.Tick(creature, _creatures);
            }
        }
    }
}

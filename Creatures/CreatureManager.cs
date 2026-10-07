using System.Collections.Generic;

namespace CaveSharp.Creatures
{
    /// <summary>
    /// Central creature controller:
    /// - Registers creatures
    /// - Runs behavior, movement, senses, combat
    /// - Provides snapshots for telemetry
    /// </summary>
    public class CreatureManager
    {
        private readonly BehaviorEngine _behavior;
        private readonly MovementEngine _movement;
        private readonly SensesEngine _senses;
        private readonly CombatEngine _combat;

        private readonly List<Creature> _creatures = new();

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

        /// <summary>
        /// Add a creature to the world.
        /// </summary>
        public void Register(Creature creature)
        {
            _creatures.Add(creature);
        }

        /// <summary>
        /// Returns all creatures for AI, combat, telemetry, etc.
        /// </summary>
        public IReadOnlyList<Creature> GetAll() => _creatures;

        /// <summary>
        /// Main creature update pipeline.
        /// </summary>
        public void Tick(float dt)
        {
            foreach (var c in _creatures)
            {
                // Biological update
                c.Tick(dt);

                // AI layers
                _behavior.Tick(c);
                _movement.Tick(c);
                _senses.Tick(c, _creatures);

                // Combat
                _combat.Tick(c, _creatures);
            }
        }

        /// <summary>
        /// Snapshot for telemetry.
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
                    Hunger = c.Hunger,
                    X = c.X,
                    Y = c.Y,
                    IsAfraid = c.IsAfraid
                });
            }

            return list;
        }
    }
}

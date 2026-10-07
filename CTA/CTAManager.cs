using System;
using System.Collections.Generic;

namespace CaveSharp.CTA
{
    /// <summary>
    /// High-level orchestrator for the CTA subsystem.
    /// Overseer and UpdateLoop interact with this class to manage CTA behavior.
    /// </summary>
    public class CTAManager
    {
        private readonly CTAEvolutionEngine _evolution;
        private readonly CTABoss _boss;

        private readonly List<CTAEntity> _entities = new();

        public CTAManager(CTAEvolutionEngine evolution, CTABoss boss)
        {
            _evolution = evolution;
            _boss = boss;
        }

        public void RegisterEntity(CTAEntity entity)
        {
            _entities.Add(entity);
            _evolution.Register(entity);

            Console.WriteLine($"[CTAManager] Registered CTA entity: {entity.Name} ({entity.Id})");
        }

        /// <summary>
        /// Called by UpdateLoop.cs
        /// </summary>
        public void Tick()
        {
            Console.WriteLine("[CTAManager] Tick");

            // Evolve corruption
            _evolution.Tick();

            // Auto-directive from boss
            string auto = _boss.AutoDirective();
            _boss.SetDirective(auto);

            // Apply boss influence to all entities
            foreach (var e in _entities)
            {
                if (!e.IsActive)
                    continue;

                // Boss aggression increases entity corruption slightly
                int pressure = (int)(_boss.AggressionLevel * 0.05f);
                if (pressure > 0)
                    e.IncreaseCorruption(pressure);
            }
        }

        public IEnumerable<CTAEntity> GetAll()
        {
            return _entities;
        }

        public CTAEntity? GetById(string id)
        {
            foreach (var e in _entities)
                if (e.Id == id)
                    return e;

            return null;
        }
    }
}

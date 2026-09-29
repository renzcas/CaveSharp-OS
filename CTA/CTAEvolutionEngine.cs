using System;
using System.Collections.Generic;

namespace CaveSharp.CTA
{
    public class CTAEvolutionEngine
    {
        private readonly List<CTAEntity> _entities = new();

        public void Register(CTAEntity entity)
        {
            _entities.Add(entity);
        }

        public void Tick()
        {
            foreach (var entity in _entities)
            {
                if (!entity.IsActive)
                    continue;

                int corruptionGain = Random.Shared.Next(1, 4);
                entity.IncreaseCorruption(corruptionGain);

                Console.WriteLine($"[CTA Evolution] {entity.Name} corruption +{corruptionGain}");
            }
        }
    }
}

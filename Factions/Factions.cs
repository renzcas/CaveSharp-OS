using System;
using System.Collections.Generic;
using CaveSharp.Creatures;

namespace CaveSharp.Factions
{
    public class Faction
    {
        public string Name { get; }
        public float Influence { get; private set; } = 10f;

        private readonly List<Creature> _members = new();

        public Faction(string name)
        {
            Name = name;
        }

        public void AddMember(Creature creature)
        {
            _members.Add(creature);
            Console.WriteLine($"[Faction] {creature.Name} joins faction {Name}.");
        }

        public void AdjustInfluence(int amount)
        {
            Influence += amount;
            if (Influence < 0) Influence = 0;
            if (Influence > 100) Influence = 100;

            Console.WriteLine($"[Faction] {Name} influence now {Influence}.");
        }

        public void SetInfluence(float influence)
        {
            if (!float.IsFinite(influence))
                throw new ArgumentOutOfRangeException(nameof(influence), "Influence must be finite.");

            Influence = Math.Clamp(influence, 0f, 100f);
        }

        public IReadOnlyList<Creature> Members => _members;
    }
}

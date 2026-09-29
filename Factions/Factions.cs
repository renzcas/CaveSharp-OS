using System;
using System.Collections.Generic;
using CaveSharp.Creatures;

namespace CaveSharp.Factions
{
    public class Faction
    {
        public string Name { get; }
        public int Influence { get; private set; } = 10;

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

            Console.WriteLine($"[Faction] {Name} influence now {Influence}.");
        }

        public IReadOnlyList<Creature> Members => _members;
    }
}

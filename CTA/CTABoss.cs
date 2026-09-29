using System;

namespace CaveSharp.CTA
{
    public class CTABoss
    {
        public string Name { get; private set; }
        public int CommandPower { get; private set; }

        private CTAEntity _coreNode;

        public CTABoss(string name, int commandPower)
        {
            Name = name;
            CommandPower = commandPower;

            // Boss creates a core corruption node
            _coreNode = new CTAEntity($"{name}-Core");
        }

        public void ExecuteTacticalDirective()
        {
            Console.WriteLine($"[CTABoss] {Name} issuing directive. CommandPower={CommandPower}");
        }
    }
}

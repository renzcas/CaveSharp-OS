namespace CaveSharp.CTA
{
    public class CTABoss : CTAEntity
    {
        public int CommandPower { get; set; } = 500;

        public void ExecuteTacticalDirective()
        {
            Console.WriteLine($"[CTA Boss] {Name} is broadcasting tactical directive. Command Power: {CommandPower}");
        }
    }
}
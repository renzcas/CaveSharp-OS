namespace CaveSharp.CTA
{
    public class CTAEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);
        public string Name { get; set; } = "Entity-Unit";
        public int Health { get; set; } = 100;
        public double ThreatLevel { get; set; } = 1.0;

        public virtual void UpdateState()
        {
            // Base tactical entity state logic
        }
    }
}
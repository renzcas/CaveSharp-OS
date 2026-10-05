namespace CaveSharpOS.CTA
{
    public class CTABoss
    {
        // Overseer expects this property
        public float AggressionLevel { get; private set; } = 0f;

        // Overseer expects this method
        public void IncreaseAggression(float amount)
        {
            AggressionLevel += amount;
        }

        // Overseer expects this method
        public void CoolDown(float dt)
        {
            AggressionLevel -= dt * 0.05f;

            if (AggressionLevel < 0)
                AggressionLevel = 0;
        }
    }
}

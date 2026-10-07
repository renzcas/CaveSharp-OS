using System;

namespace CaveSharp.CTA
{
    /// <summary>
    /// CTABoss is the central command node of the CTA subsystem.
    /// Overseer.cs interacts with this class to increase aggression,
    /// apply pressure responses, and manage CTA escalation cycles.
    /// </summary>
    public class CTABoss
    {
        // Identity
        public string Name { get; }

        // CTA aggression level (Overseer uses this)
        public float AggressionLevel { get; private set; }

        // CTA power (used in Overseer threat calculations)
        public int Power { get; private set; }

        // Current tactical directive
        public string CurrentDirective { get; private set; }

        public CTABoss(string name, float initialAggression = 0f, int initialPower = 50)
        {
            Name = name;
            AggressionLevel = initialAggression;
            Power = initialPower;
            CurrentDirective = "Hold position";
        }

        /// <summary>
        /// Overseer calls this to escalate CTA aggression.
        /// </summary>
        public void IncreaseAggression(float amount = 5f)
        {
            AggressionLevel += amount;
            if (AggressionLevel > 100) AggressionLevel = 100;

            Console.WriteLine($"[CTABoss] Aggression increased to {AggressionLevel}");
        }

        /// <summary>
        /// Natural decay of aggression over time.
        /// </summary>
        public void CoolDown(float dt)
        {
            AggressionLevel -= dt * 0.1f;
            if (AggressionLevel < 0) AggressionLevel = 0;
        }

        /// <summary>
        /// Overseer calls this when threat is high.
        /// </summary>
        public void IncreasePower(int amount = 1)
        {
            Power += amount;
            if (Power > 999) Power = 999;

            Console.WriteLine($"[CTABoss] Power increased to {Power}");
        }

        /// <summary>
        /// CTACommandServer sends directives here.
        /// </summary>
        public void SetDirective(string directive)
        {
            CurrentDirective = directive;
            Console.WriteLine($"[CTABoss] Directive set: {directive}");
        }

        /// <summary>
        /// CTA Boss issues tactical directives automatically.
        /// Overseer uses this in telemetry.
        /// </summary>
        public string AutoDirective()
        {
            if (AggressionLevel > 70)
                return "Deploy pressure units";

            if (AggressionLevel > 40)
                return "Increase surveillance";

            return "Monitor corruption nodes";
        }
    }
}

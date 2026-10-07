namespace CaveSharp.CTA
{
    /// <summary>
    /// Strongly-typed CTA directives used by CTABoss and CTACommandServer.
    /// Overseer and WebUI can read these for telemetry.
    /// </summary>
    public static class CTADirective
    {
        public const string HoldPosition = "Hold position";
        public const string MonitorCorruption = "Monitor corruption nodes";
        public const string IncreaseSurveillance = "Increase surveillance";
        public const string DeployPressureUnits = "Deploy pressure units";
        public const string FullMobilization = "Full mobilization";

        /// <summary>
        /// Converts raw text into a known directive if possible.
        /// Falls back to raw text for custom commands.
        /// </summary>
        public static string Normalize(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return HoldPosition;

            raw = raw.Trim().ToLower();

            return raw switch
            {
                "hold" or "hold position" => HoldPosition,
                "monitor" or "monitor corruption" => MonitorCorruption,
                "surveillance" or "increase surveillance" => IncreaseSurveillance,
                "pressure" or "deploy pressure units" => DeployPressureUnits,
                "mobilize" or "full mobilization" => FullMobilization,
                _ => raw // custom directive
            };
        }
    }
}

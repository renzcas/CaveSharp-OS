namespace CaveSharpOS.BigAnimal;

public sealed record WorldHeart
{
    public float Stability { get; init; } = 1f;
    public float ConflictRisk { get; init; }
    public float EnergyPulse { get; init; }
    public float IndustrialStress { get; init; }
    public float ClimateStress { get; init; }
    public float SocialCohesion { get; init; } = 1f;
    public float MisinformationPressure { get; init; }

    public void Validate()
    {
        ValidateUnitInterval(nameof(Stability), Stability);
        ValidateUnitInterval(nameof(ConflictRisk), ConflictRisk);
        ValidateUnitInterval(nameof(EnergyPulse), EnergyPulse);
        ValidateUnitInterval(nameof(IndustrialStress), IndustrialStress);
        ValidateUnitInterval(nameof(ClimateStress), ClimateStress);
        ValidateUnitInterval(nameof(SocialCohesion), SocialCohesion);
        ValidateUnitInterval(nameof(MisinformationPressure), MisinformationPressure);
    }

    private static void ValidateUnitInterval(string name, float value)
    {
        if (!float.IsFinite(value) || value is < 0f or > 1f)
            throw new ArgumentOutOfRangeException(name, value, "WorldHeart values must be finite and between 0 and 1.");
    }
}

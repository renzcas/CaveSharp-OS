namespace CaveSharp.Realm
{
    public class RealmRegion
    {
        // How strongly this region responds to consciousness waves
        public float ResonanceFactor { get; set; } = 1.0f;

        // Current harmonic intensity (computed by MindWavesRealmHarmonics)
        public float HarmonicLevel { get; set; }

        // Textual ambience description (Stillness, Calm, Flow, Vivid, Resonant)
        public string Ambience { get; set; } = "Neutral";

        // Optional: region name for debugging or display
        public string Name { get; set; } = "Unnamed Region";
    }
}

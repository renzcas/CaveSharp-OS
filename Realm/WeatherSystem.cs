namespace CaveSharp.Realm
{
    public class WeatherSystem
    {
        public string CurrentClimate { get; set; } = "Void-Mist";
        public double AmbientPressure { get; set; } = 104.5;

        public void ShiftClimate()
        {
            CurrentClimate = "Quantum-Storm";
            AmbientPressure += 12.3;
            Console.WriteLine($"[Realm] Weather shift detected. Climate: {CurrentClimate}, Pressure: {AmbientPressure}kPa");
        }
    }
}
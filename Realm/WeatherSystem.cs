using System;

namespace CaveSharp.Realm
{
    public enum WeatherType
    {
        Clear,
        Rain,
        Storm,
        Fog,
        Heatwave
    }

    public class WeatherSystem
    {
        public WeatherType CurrentWeather { get; private set; } = WeatherType.Clear;

        public void Tick()
        {
            int roll = Random.Shared.Next(0, 100);

            if (roll < 60)
                CurrentWeather = WeatherType.Clear;
            else if (roll < 75)
                CurrentWeather = WeatherType.Rain;
            else if (roll < 85)
                CurrentWeather = WeatherType.Fog;
            else if (roll < 95)
                CurrentWeather = WeatherType.Storm;
            else
                CurrentWeather = WeatherType.Heatwave;

            Console.WriteLine($"[Realm Weather] Current weather: {CurrentWeather}");
        }
    }
}

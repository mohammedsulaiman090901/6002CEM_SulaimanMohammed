using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        bool continueRunning = true;

        while (continueRunning)
        {
            Console.WriteLine("Enter a city name:");
            string? city = Console.ReadLine()?.Trim();

            Console.WriteLine("Select the temperature unit (C for Celsius, F for Fahrenheit, K for Kelvin):");
            string? unitChoice = Console.ReadLine()?.Trim().ToUpper();
            string unit = unitChoice switch
            {
                "F" => "imperial",
                "K" => "standard",
                _ => "metric"
            };

            if (string.IsNullOrEmpty(city))
            {
                Console.WriteLine("City name cannot be empty.");
                continue;
            }

            WeatherService weatherService = new WeatherService(unit);
            try
            {
                string forecastJson = await weatherService.GetWeatherForecastAsync(city);

                if (string.IsNullOrEmpty(forecastJson))
                {
                    Console.WriteLine("No weather data returned. Please check the city name and try again.");
                    continue;
                }

                WeatherData? weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherData>(forecastJson);

                if (weatherData != null && weatherData.Forecasts.Length > 0)
                {
                    Console.WriteLine($"Weather forecast for {city}:");
                    foreach (var forecast in weatherData.Forecasts)
                    {
                        Console.WriteLine($"Date and Time: {forecast.DateTime}");
                        Console.WriteLine($"Temperature: {forecast.Main?.Temperature ?? 0}°{(unitChoice == "F" ? "F" : unitChoice == "K" ? "K" : "C")}");
                        Console.WriteLine($"Humidity: {forecast.Main?.Humidity ?? 0}%");
                        Console.WriteLine($"Description: {forecast.Weather[0].Description ?? "No description available"}");
                        Console.WriteLine($"Wind Speed: {forecast.Wind?.Speed ?? 0} m/s");
                        Console.WriteLine("--------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Weather data could not be parsed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.WriteLine("Do you want to check another city? (yes/no)");
            string? answer = Console.ReadLine()?.Trim().ToLower();
            continueRunning = (answer == "yes");
        }

        Console.WriteLine("Thank you for using the Weather Forecast application!");
    }
}
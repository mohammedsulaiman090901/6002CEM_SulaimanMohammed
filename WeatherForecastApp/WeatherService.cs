using System;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _unit;

    public WeatherService(string unit)
    {
        _httpClient = new HttpClient();
        _unit = unit;
    }

    public async Task<string> GetWeatherForecastAsync(string city)
    {
        string apiKey = "76c963aa013325d00dfa5e8b40adf829";  // Your provided API key
        string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units={_unit}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Unable to fetch weather data. Status Code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException e)
        {
            throw new Exception("Network error occurred while fetching weather data.", e);
        }
    }
}
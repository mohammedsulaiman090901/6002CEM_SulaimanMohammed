using Newtonsoft.Json;
using System;

public class WeatherData
{
    [JsonProperty("list")]
    public WeatherForecast[] Forecasts { get; set; } = Array.Empty<WeatherForecast>();
}

public class WeatherForecast
{
    [JsonProperty("dt_txt")]
    public string DateTime { get; set; } = string.Empty;

    [JsonProperty("main")]
    public MainData? Main { get; set; } = new MainData();

    [JsonProperty("weather")]
    public WeatherDescription[] Weather { get; set; } = Array.Empty<WeatherDescription>();

    [JsonProperty("wind")]
    public Wind? Wind { get; set; } = new Wind();
}

public class MainData
{
    [JsonProperty("temp")]
    public double? Temperature { get; set; }

    [JsonProperty("humidity")]
    public int? Humidity { get; set; }
}

public class WeatherDescription
{
    [JsonProperty("description")]
    public string? Description { get; set; } = string.Empty;
}

public class Wind
{
    [JsonProperty("speed")]
    public double? Speed { get; set; }
}
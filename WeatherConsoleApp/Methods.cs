using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherConsoleApp
{
    public static class Methods
    {
        //methods
        public static double KelvinToCelsius(double kelvinTemp)
        {
            return Math.Floor(kelvinTemp - 273.15);
        }

        public static async Task<HttpResponseMessage> GetDataAsync(HttpClient client, string city, string forecastType)
        {
            var weatherUri = $"{ Config.BaseUri() }/{ forecastType }?q={city}&appid={ Config.ApiKey() }";

            return await client.GetAsync(weatherUri);
        }

        public static async Task<WeatherDetails.Root> ParseResponseWeather(HttpResponseMessage result)
        {
            //httpresponsemessage to string 
            var resultString = await result.Content.ReadAsStringAsync();

            //parsing jsondata to c#
            return JsonConvert.DeserializeObject<WeatherDetails.Root>(resultString);
        }

        public static async Task<Forecast.Root> ParseResponseForecast(HttpResponseMessage result)
        {
            var resultStringForecast = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Forecast.Root>(resultStringForecast);
        }

        public static string CreateDescription(WeatherDetails.Root weatherDetail)
        {
            string description = string.Empty;

            foreach (WeatherDetails.Weather item in weatherDetail.weather)
            {
                if (item != weatherDetail.weather[weatherDetail.weather.Count - 1])
                {
                    description += $"{item.description}, ";
                }
                else
                {
                    description += item.description;
                }
            }

            return description;
        }

        public static string CreateForecastDescription(Forecast.Root forecastDetail)
        {
            string forecastDate = string.Empty;

            foreach(Forecast.List item2 in forecastDetail.list)
            {
                if (item2 != forecastDetail.list[forecastDetail.list.Count - 1])
                {
                    forecastDate += $"{item2.dt_txt}, ";
                }
                else
                {
                    forecastDate += item2.dt_txt;
                }
            }
            return forecastDate;
        }
       

        public static async Task<bool> Process()
        {
            string forecastType = ConsolePrompts.GetForecastType();
            string userCity = ConsolePrompts.GetLocation(forecastType);

            //creating HTTP request with HttpClient
            using var client = new HttpClient();

            var result = await GetDataAsync(client, userCity, forecastType);

            if (result.IsSuccessStatusCode)
            {
                
                switch (forecastType)
                {
                    case "weather":
                        var weatherDetail = await ParseResponseWeather(result);
                        // temp and etc 
                        var maxTemp = KelvinToCelsius(weatherDetail.main.temp_max);
                        var minTemp = KelvinToCelsius(weatherDetail.main.temp_min);
                        var country = weatherDetail.sys.country;
                        var windSpeed = weatherDetail.wind.speed;

                        string description = CreateDescription(weatherDetail);

                        return ConsolePrompts.CheckWeather(userCity,
                                                    country,
                                                    description,
                                                    maxTemp,
                                                    minTemp,
                                                    windSpeed);
                        
                    case "forecast":
                        var forecastDetail = await ParseResponseForecast(result);

                        // date and etc
                        foreach(var item in forecastDetail.list)
                        {
                            ConsolePrompts.ShowForecastResult(userCity, item.dt_txt, KelvinToCelsius(item.main.temp_max), KelvinToCelsius(item.main.temp_min));
                        }
                        return true;
                  
                }
                return true;
                
            }
            else
            {
                Console.WriteLine("The location you entered was not found.");
                return false;
            }

        }
    }
}

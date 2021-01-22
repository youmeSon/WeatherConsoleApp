using System;

namespace WeatherConsoleApp
{
    public static class ConsolePrompts
    {

        public static string GetForecastType()
        {
            string forecastTypeAnswer;

            Console.WriteLine("What do you want to know in your city? (Enter weather or forecast)");

            forecastTypeAnswer = Console.ReadLine();

            while (forecastTypeAnswer != "weather" && forecastTypeAnswer != "forecast")
            {
                Console.WriteLine("Please enter either 'weather' or 'forecast'.");

                forecastTypeAnswer = Console.ReadLine();
            }

            if(forecastTypeAnswer == "weather")
            {
                return forecastTypeAnswer;
            }
            else
            {
                return forecastTypeAnswer;
            }           
        }
        public static string GetLocation(string forecast)
        {
            Console.WriteLine($"I can tell you the {forecast} in your city!");
            Console.WriteLine("Where do you live? Could you enter the city and country?");
            return Console.ReadLine();
        }

        public static void ShowWeatherResult(string userCity, string description, double maxTemp, double minTemp, double windSpeed)
        {
            Console.WriteLine($"{userCity}'s weather is {description}");
            Console.WriteLine($"The maximum temperature is {maxTemp} celsius, and the minimum temperature is {minTemp} celsius");
            Console.WriteLine($"The wind speed is {windSpeed}m/s");
        }

        public static void ShowForecastResult(string userCity, string forecastDate, double maxTemp2, double minTemp2)
        {
            Console.WriteLine($"{userCity}'s date is {forecastDate}");
            Console.WriteLine($"The maximum temperature is {maxTemp2} celsius, and the minimum temperature is {minTemp2} celsius");
            
        }

        public static bool CheckWeather(string userCity, string country, string description, double maxTemp, double minTemp, double windSpeed)
        {
            Console.WriteLine($"Is {userCity}, {country} the correct location? (Please enter 'yes' or 'no')");
            string correct = Console.ReadLine();
            if (correct == "yes")
            {
                ShowWeatherResult(userCity, description, maxTemp, minTemp, windSpeed);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckForecast(string userCity, string country2, string forecastDate, double maxTemp2, double minTemp2)
        {
            Console.WriteLine($"Is { userCity}, { country2 } the correct location ? (Please enter 'yes' or 'no')");
            string correct = Console.ReadLine();
            if (correct == "yes")
            {
                ShowForecastResult(userCity, forecastDate, maxTemp2, minTemp2);
                return true;
            }
            else
            {
                return false;
            }
        }
            
    }
}

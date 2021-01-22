using System.Threading.Tasks;

namespace WeatherConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool result = await Methods.Process();

            while(result == false)
            {
                result = await Methods.Process();
            }
        }

    }
}

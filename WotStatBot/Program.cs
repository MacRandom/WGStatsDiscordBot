using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WotStatBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                .Build();

            var bot = new BotCore(configuration);

            await bot.Start();
        }
    }
}

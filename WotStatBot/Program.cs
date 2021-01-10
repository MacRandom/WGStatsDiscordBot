using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WotStatBot.Wargaming;

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

            CancellationTokenSource cancelationToken = new CancellationTokenSource();
            WargamingApi wargamingApi = new WargamingApi(configuration);

            var bot = new BotCore(configuration, cancelationToken, wargamingApi);

            await bot.Start();
        }
    }
}

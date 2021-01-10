using System.IO;
using System.Threading;
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

            CancellationTokenSource cancelationToken = new CancellationTokenSource();

            var bot = new BotCore(configuration, cancelationToken);

            await bot.Start();
        }
    }
}

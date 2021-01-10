using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;

namespace WotStatBot
{
    internal class BotCore
    {
        private IConfigurationRoot _configuration;
        private CancellationTokenSource _cancellationToken;
        private DiscordClient _discordClient;
        private InteractivityModule _interactivity;
        private CommandsNextModule _commands;

        public BotCore(IConfigurationRoot configuration, CancellationTokenSource cancelationToken)
        {
            _configuration = configuration;
            _cancellationToken = cancelationToken;
        }

        public async Task Start()
        {
            try
            {
                _discordClient = new DiscordClient(new DiscordConfiguration
                {
                    TokenType = TokenType.Bot,
                    Token = _configuration["Discord:Token"]
                });

                _interactivity = _discordClient.UseInteractivity(new InteractivityConfiguration
                {
                    PaginationBehaviour = TimeoutBehaviour.Delete,
                    PaginationTimeout = TimeSpan.FromSeconds(30),
                    Timeout = TimeSpan.FromSeconds(30)
                });

                var deps = BuildDeps();

                _commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
                {
                    StringPrefix = _configuration["Discord:CommandPrefix"],
                    Dependencies = deps
                });

                await RunAsync();
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.ToString());
            }
        }

        private async Task RunAsync()
        {
            while (!_cancellationToken.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromMinutes(1));
        }

        private DependencyCollection BuildDeps()
        {
            using var deps = new DependencyCollectionBuilder();

            deps.AddInstance(_interactivity)
                .AddInstance(_cancellationToken)
                .AddInstance(_configuration)
                .AddInstance(_discordClient);

            return deps.Build();
        }
    }
}

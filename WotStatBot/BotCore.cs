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

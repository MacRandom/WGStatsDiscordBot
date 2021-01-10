using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WotStatBot
{
    internal class BotCore
    {
        private IConfigurationRoot _configuration;
        private CancellationTokenSource _cancellationToken;

        public BotCore(IConfigurationRoot configuration, CancellationTokenSource cancelationToken)
        {
            _configuration = configuration;
            _cancellationToken = cancelationToken;
        }

        public async Task Start()
        {

        }
    }
}

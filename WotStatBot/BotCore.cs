using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WotStatBot
{
    internal class BotCore
    {
        private IConfigurationRoot _configuration;

        public BotCore(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public async Task Start()
        {

        }
    }
}

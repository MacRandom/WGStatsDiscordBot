using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WotStatBot.Wargaming
{
    internal class WargamingApi
    {
        private IConfigurationRoot _configuration;
        private string _appId;
        private UrlConstructor _urlConstructor;

        public WargamingApi(IConfigurationRoot configuration)
        {
            _configuration = configuration;
            _appId = _configuration["Wargaming:AppId"];
            _urlConstructor = new UrlConstructor(_appId);
        }
    }
}

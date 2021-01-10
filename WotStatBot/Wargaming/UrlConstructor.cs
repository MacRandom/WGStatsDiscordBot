using System;
using System.Collections.Generic;
using System.Text;

namespace WotStatBot.Wargaming
{
    internal class UrlConstructor
    {
        private const string _wotUrlPrefix = @"https://api.worldoftanks.ru/wot/";
        private const string _listSuffix = @"account/list/";
        private const string _infoSuffix = @"account/info/";
        private const string _appIdField = @"?application_id=";
        private const string _searchField = @"&search=";
        private string _appId;

        public UrlConstructor(string appId)
        {
            _appId = appId;
        }

        public string GetFindPlayerUrl(string name)
            => _wotUrlPrefix + _listSuffix + _appIdField + _appId + _searchField + name;
    }
}

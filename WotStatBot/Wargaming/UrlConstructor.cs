namespace WotStatBot.Wargaming
{
    internal class UrlConstructor
    {
        private const string _wotUrlPrefix = @"https://api.worldoftanks.ru/wot/";
        private const string _listSuffix = @"account/list/";
        private const string _infoSuffix = @"account/info/";
        private const string _appIdField = @"?application_id=";
        private const string _accIdField = @"&account_id=";
        private const string _searchField = @"&search=";
        private string _appId;

        public UrlConstructor(string appId)
        {
            _appId = appId;
        }

        public string GetFindPlayerUrl(string name)
            => _wotUrlPrefix + _listSuffix + _appIdField + _appId + _searchField + name;

        public string GetPlayerStatsRandomUrl(string id)
            => _wotUrlPrefix + _infoSuffix + _appIdField + _appId + _accIdField + id +
            "&extra=statistics.random" +
            "&fields=statistics.trees_cut,+" +
            "statistics.random.avg_damage_assisted,+" +
            "statistics.random.avg_damage_assisted_radio,+" +
            "statistics.random.avg_damage_assisted_track,+" +
            "statistics.random.avg_damage_blocked,+" +
            "statistics.random.battles,+" +
            "statistics.random.draws,+" +
            "statistics.random.frags,+" +
            "statistics.random.hits_percents,+" +
            "statistics.random.losses,+" +
            "statistics.random.survived_battles,+" +
            "statistics.random.wins,+" +
            "statistics.random.damage_dealt";
    }
}

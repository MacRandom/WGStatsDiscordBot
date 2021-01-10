using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WotStatBot.Models;

namespace WotStatBot.Wargaming
{
    public class WargamingApi
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

        public async Task<Player> GetPlayer(string name)
        {
            HttpClient client = new HttpClient();
            string urlRequest = _urlConstructor.GetFindPlayerUrl(name);

            var response = await client.GetAsync(urlRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic parsed = JsonConvert.DeserializeObject(responseString);

            if (parsed.status == "ok")
            {
                int count = parsed.meta.count;
                if (count > 0)
                {
                    return new Player
                    {
                        Id = parsed.data[0].account_id,
                        Name = parsed.data[0].nickname
                    };
                }
                else
                {
                    throw new Exception("Игрок не найден");
                }
            }
            else
            {
                throw new Exception("Неверный запрос");
            }
        }

        public async Task<PlayerStats> GetStat(string id)
        {
            HttpClient client = new HttpClient();
            string urlRequest = _urlConstructor.GetPlayerStatsRandomUrl(id);

            var response = await client.GetAsync(urlRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic parsed = JsonConvert.DeserializeObject(responseString);

            if (parsed.status == "ok")
            {
                int count = parsed.meta.count;
                if (count > 0)
                {
                    return new PlayerStats
                    {
                        TreesCut = parsed.data[0].statistics.random.avg_damage_assisted,
                        AverageDamageAssisted = parsed.data[0].statistics.random.avg_damage_assisted,
                        AverageDamageAssistedRadio = parsed.data[0].statistics.random.avg_damage_assisted_radio,
                        AverageDamageAssistedTrack = parsed.data[0].statistics.random.avg_damage_assisted_track,
                        AverageDamageBlocked = parsed.data[0].statistics.random.avg_damage_blocked,
                        BattleCount = parsed.data[0].statistics.random.battles,
                        DrawCount = parsed.data[0].statistics.random.draws,
                        FragsCount = parsed.data[0].statistics.random.frags,
                        HitsPercent = parsed.data[0].statistics.random.hits_percents,
                        LoseCount = parsed.data[0].statistics.random.losses,
                        SurviveCount = parsed.data[0].statistics.random.survived_battles,
                        WinCount = parsed.data[0].statistics.random.wins
                    };
                }
                else
                {
                    throw new Exception("Игрок не найден");
                }
            }
            else
            {
                throw new Exception("Неверный запрос");
            }
        }
    }
}

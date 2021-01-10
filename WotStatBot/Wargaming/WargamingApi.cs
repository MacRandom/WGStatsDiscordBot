using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            
            var jObj = JObject.Parse(responseString);
            string status = jObj.SelectToken("status").ToString();
            dynamic parsed = JsonConvert.DeserializeObject(jObj.SelectToken($"data.{id}").ToString());

            if (status == "ok")
            {
                return new PlayerStats
                {
                    TreesCut = parsed.statistics.trees_cut,
                    AverageDamageAssisted = parsed.statistics.random.avg_damage_assisted,
                    AverageDamageAssistedRadio = parsed.statistics.random.avg_damage_assisted_radio,
                    AverageDamageAssistedTrack = parsed.statistics.random.avg_damage_assisted_track,
                    AverageDamageBlocked = parsed.statistics.random.avg_damage_blocked,
                    BattleCount = parsed.statistics.random.battles,
                    DrawCount = parsed.statistics.random.draws,
                    FragsCount = parsed.statistics.random.frags,
                    HitsPercent = parsed.statistics.random.hits_percents,
                    LoseCount = parsed.statistics.random.losses,
                    SurviveCount = parsed.statistics.random.survived_battles,
                    WinCount = parsed.statistics.random.wins,
                    DamageDealt = parsed.statistics.random.damage_dealt
                };
            }
            else
            {
                throw new Exception("Неверный запрос");
            }
        }
    }
}

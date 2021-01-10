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
    }
}

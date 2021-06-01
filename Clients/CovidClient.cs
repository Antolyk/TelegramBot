using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Models;

namespace TelegramBot.Clients
{
    class CovidClient
    {
        private HttpClient _client;
        private static string _adress;
        public CovidClient()
        {
            _adress = Constants.adress;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }
        public async Task<string> GetStatisticByCountry(string countryName)
        {
            var responce = await _client.GetAsync($"/covidstatistic/country?country={countryName}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;           
            return content;
        }
        public async Task<string> GetStatisticByWorld()
        {
            var responce = await _client.GetAsync("/covidstatistic/all");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;           
            return content;
        }
        public async Task<string> GetAnswerByNumber(string countryName, int number, string descript)
        {
            var responce = await _client.GetAsync($"/covidstatistic/controlByNumber?country={countryName}&number={number}&description={descript}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;          
            return content;
        }
        public async Task<string> GetInfoFromNotficationDB(int id)
        {
            var responce = await _client.GetAsync($"/covidstatistic/db?id={id}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            return content;
        }
        public async Task<AllCountriesModels> GetTop10(string descript)
        {
            var responce = await _client.GetAsync($"/covidstatistic/top?description={descript}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<AllCountriesModels>(content);

            return result;
        }
        public async Task<ContinentStatistic> GetStatisticByContinent(string continent)
        {
            var responce = await _client.GetAsync($"/covidstatistic/continent?continent={continent}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ContinentStatistic>(content);
            return result;
        }

    }
}

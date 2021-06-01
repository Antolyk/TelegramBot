using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class WorldStatistic
    {
        [JsonProperty(PropertyName = "world_total")]
        public World_total WorldStatisticModel { get; }
    }
    public class World_total
    {
        public string Total_cases { get;}
        public string New_cases { get;}
        public string Total_deaths { get;}
        public string New_deaths { get;}
        public string Total_recovered { get;}
        public string Active_cases { get;}
        public string Serious_critical { get;}
    }
}

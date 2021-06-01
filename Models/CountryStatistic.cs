using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class CountryStatistic
    {    
        public Statistic StatisticModel { get; set; }
    }
    public class Statistic
    {
        public string Country { get; set; }
        public int Confirmed { get; set; }
        public int Recovered { get; set; }
        public int Critical { get; set; }
        public int Deaths { get; set; }       
    }
}

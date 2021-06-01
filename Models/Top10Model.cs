using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class AllCountriesModels
    {
        public Countries_Stat[] countries_stat { get; set; }
    }

    public class Countries_Stat
    {
        public string country_name { get; set; }
        public string cases { get; set; }
    }
}

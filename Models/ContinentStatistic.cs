using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class ContinentStatistic
    {
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string continent { get; set; }
        public string totalCases { get; set; }
        public string totalDeaths { get; set; }
        public string totalRecovered { get; set; }
    }
}

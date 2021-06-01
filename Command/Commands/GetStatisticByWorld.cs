using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Web.Helpers;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Command.Commands
{
    class GetStatisticByWorld : Command
    {
        private readonly CovidClient _covidClient = new CovidClient();
        public override TelegramBotClient _client { get; set; }
        public override string Name { get; set; } = "/byworld";

        public override async void Execute(Message message, TelegramBotClient client, List<Command> _commands)
        {
            CovidClient cl = new CovidClient();           
            var result = await cl.GetStatisticByWorld();
            var parsedData = Json.Decode(result);
            await client.SendTextMessageAsync(message.From.Id, $"<b><i>Statistic by World</i></b>\n\n" +
                $"<b>Всего заболевших</b>: <i>{parsedData.total_cases}</i>\n\n" +
                $"<b>Новые случаи</b>: <i>{parsedData.new_cases}</i>\n\n" +
                $"<b>Всего смертей</b>: <i>{parsedData.total_deaths}</i>\n\n" +
                $"<b>Новые случаи смерти</b>: <i>{parsedData.new_deaths}</i>\n\n" +
                $"<b>Всего излеченных</b>: <i>{parsedData.total_recovered}</i>\n\n" +
                $"<b>Активные случаи</b>: <i>{parsedData.active_cases}</i>\n\n" +
                $"<b>Серьезное критическое состояние</b>: <i>{parsedData.serious_critical}</i>", parseMode: ParseMode.Html);
        }
    }
}

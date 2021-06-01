using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;
using TelegramBot.Models;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using Newtonsoft.Json;

namespace TelegramBot.Command.Commands
{
    class GetStatisticByCountry : Command
    {
        public override TelegramBotClient _client { get; set; }
        public override string Name { get; set; } = "/bycountry";
        private string CountryName { get; set; }
        public static List<Command> commands { get; set; }
        public override async void Execute(Message message, TelegramBotClient _client, List<Command> _commands)
        {
            commands = _commands;
            this._client = _client;
            await _client.SendTextMessageAsync(message.From.Id, "Введите название страны");
            this._client.OnMessage += GetString;
        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            CountryName = e.Message.Text;
            foreach (Command command in commands)
            {
                if (CountryName == command.Name)
                {
                    return;
                }
            }
            CovidClient cl = new CovidClient();
            try
            {
                var result = await cl.GetStatisticByCountry(CountryName);
                SendInf(result, e.Message);
            }
            catch
            {
                await _client.SendTextMessageAsync(e.Message.From.Id, "Неправильно введена информация");
            }
            _client.OnMessage -= GetString;

        }
        protected async void SendInf(string results, Message message)
        {
            var parsedData = JsonConvert.DeserializeObject<Statistic>(results);
            await _client.SendTextMessageAsync(message.From.Id, $"<i><b>{parsedData.Country} Statistic</b></i>\n\n" +
                $"<b>Подтвержденные случаи</b>: <i>{parsedData.Confirmed}</i>\n\n" +
                $"<b>Выздоровели</b>: <i>{parsedData.Recovered}</i>\n\n" +
                $"<b>В критическом состоянии</b>: <i>{parsedData.Critical}</i>\n\n" +
                $"<b>Погибли</b>: <i>{parsedData.Deaths}</i>", parseMode: ParseMode.Html);
        }
    }
}

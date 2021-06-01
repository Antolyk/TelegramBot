using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Clients;
using TelegramBot.Models;

namespace TelegramBot.Command.Commands
{
    class GetStatisticByContinent : Command
    {
        public override TelegramBotClient _client { get; set; }
        public override string Name { get; set; } = "/bycontinent";
        private string ContinentName { get; set; }
        public static List<Command> commands { get; set; }

        public override async void Execute(Message message, TelegramBotClient client, List<Command> _commands)
        {
            commands = _commands;
            this._client = client;
            await _client.SendTextMessageAsync(message.From.Id, "Введите название континента");
            await _client.SendTextMessageAsync(message.From.Id, "North America, South America, Europe, Asia, Africa, Oceania");
            this._client.OnMessage += GetString;

        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            ContinentName = e.Message.Text;
            foreach (Command command in commands)
            {
                if (ContinentName == command.Name)
                {
                    return;
                }
            }
            CovidClient cl = new CovidClient();
            try
            {
                var result = await cl.GetStatisticByContinent(ContinentName);
                SendInf(result, e.Message);
            }
            catch
            {
                await _client.SendTextMessageAsync(e.Message.From.Id, "Неправильно введена информация");
            }                                                
            _client.OnMessage -= GetString;
        }
        protected async void SendInf(ContinentStatistic results, Message message)
        {
            await _client.SendTextMessageAsync(message.From.Id, $"<b><i>{results.result[0].continent} Statistic</i></b>\n\n" +
                $"<b>Подтвержденные случаи</b>: <i>{results.result[0].totalCases}</i>\n\n" +
                $"<b>Выздоровели</b>: <i>{results.result[0].totalRecovered}</i>\n\n" +
                $"<b>Погибли</b>: <i>{results.result[0].totalDeaths}</i>", parseMode: ParseMode.Html);
        }
    }
}

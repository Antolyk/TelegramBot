using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Clients;
using TelegramBot.Models;

namespace TelegramBot.Command.Commands
{
    class GetTop10 : Command
    {
        public override TelegramBotClient _client { get; set; }
        public override string Name { get; set; } = "/gettop";
        public string Description { get; set; }
        public static List<Command> commands { get; set; }

        public override async void Execute(Message message, TelegramBotClient client, List<Command> _commands)
        {
            commands = _commands;
            this._client = client;
            await client.SendTextMessageAsync(message.From.Id, "Напишите 'больше', если хотите топ стран с найбольшим кол-вом заболевших или 'меньше', если найменьшим ");
            client.OnMessage += GetString;
        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            Description = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Description == command.Name)
                {
                    return;
                }
            }
            CovidClient cl = new CovidClient();
            try
            {
                var result = await cl.GetTop10(Description);
                SendInf(result, e.Message);
            }
            catch
            {
                await _client.SendTextMessageAsync(e.Message.From.Id, "Неправильно введена информация");
            }        
            
            _client.OnMessage -= GetString;
        }
        protected async void SendInf(AllCountriesModels results, Message message)
        {
            await _client.SendTextMessageAsync(message.From.Id, $" <b><i>Top 10</i></b> \n\n" +
               $"1.<b>{results.countries_stat[0].country_name}</b> - <i>{results.countries_stat[0].cases}</i> заболевших\n\n" +
               $"2.<b>{results.countries_stat[1].country_name}</b> - <i>{results.countries_stat[1].cases}</i> заболевших\n\n" +
               $"3.<b>{results.countries_stat[2].country_name}</b> - <i>{results.countries_stat[2].cases}</i> заболевших\n\n" +
               $"4.<b>{results.countries_stat[3].country_name}</b> - <i>{results.countries_stat[3].cases}</i> заболевших\n\n" +
               $"5.<b>{results.countries_stat[4].country_name}</b> - <i>{results.countries_stat[4].cases}</i> заболевших\n\n" +
               $"6.<b>{results.countries_stat[5].country_name}</b> - <i>{results.countries_stat[5].cases}</i> заболевших\n\n" +
               $"7.<b>{results.countries_stat[6].country_name}</b> - <i>{results.countries_stat[6].cases}</i> заболевших\n\n" +
               $"8.<b>{results.countries_stat[7].country_name}</b> - <i>{results.countries_stat[7].cases}</i> заболевших\n\n" +
               $"9.<b>{results.countries_stat[8].country_name}</b> - <i>{results.countries_stat[8].cases}</i> заболевших\n\n" +
               $"10.<b>{results.countries_stat[9].country_name}</b> - <i>{results.countries_stat[9].cases}</i> заболевших", parseMode: ParseMode.Html);           
        }
    }
}

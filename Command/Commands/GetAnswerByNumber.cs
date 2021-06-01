using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;
using TelegramBot.Command;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;
using System.Web.WebPages;

namespace TelegramBot.Command.Commands
{
    class GetAnswerByNumber : Command
    {
        private readonly CovidClient _covidClient = new CovidClient();
        public override TelegramBotClient _client { get; set; }
        public override string Name { get; set; } = "/bynumber";
        private string CountryName { get; set; }
        private int Number { get; set; }
        private string Description { get; set; }
        public static List<Command> commands { get; set; }
        public override async void Execute(Message message, TelegramBotClient _client, List<Command> _commands)
        {
            commands = _commands;
            this._client = _client;
            await _client.SendTextMessageAsync(message.From.Id, "Введите название страны");
            this._client.OnMessage += GetStringCountry;
        }
        private async void GetStringCountry(object sender, MessageEventArgs e)
        {
            CountryName = e.Message.Text;
            foreach (Command command in commands)
            {
                if (CountryName == command.Name)
                {
                    return;
                }
            }
            _client.OnMessage -= GetStringCountry;
            await _client.SendTextMessageAsync(e.Message.From.Id, "Введите предположительно число заболевших");
            _client.OnMessage += GetIntNumber;
        }
        private async void GetIntNumber(object sender, MessageEventArgs e)
        {
            try
            {
                Number = Int32.Parse(e.Message.Text);
            }
            catch
            {
                await _client.SendTextMessageAsync(e.Message.From.Id, "Неправильно введена информация");
            }
            foreach (Command command in commands)
            {
                if (Number.ToString() == command.Name)
                {
                    return;
                }
            }
            _client.OnMessage -= GetIntNumber;
            await _client.SendTextMessageAsync(e.Message.From.Id, "Введите больше/меньше");
            _client.OnMessage += GetStringDescription;
        }
        private async void GetStringDescription(object sender, MessageEventArgs e)
        {
            Description = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Description == command.Name)
                {
                    return;
                }
            }
            _client.OnMessage -= GetStringDescription;
            CovidClient covidClient = new CovidClient();
            var result = await covidClient.GetAnswerByNumber(CountryName, Number, Description);
            
            await _client.SendTextMessageAsync(e.Message.From.Id, result);
         
        }
    }
}

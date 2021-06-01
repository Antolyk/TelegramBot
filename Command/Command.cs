using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;

namespace TelegramBot.Command
{
    public abstract class Command
    {
        public abstract TelegramBotClient _client { get; set; }
        private readonly CovidClient _covidClient = new CovidClient();
        public abstract string Name { get; set; }
        public abstract void Execute(Message message, TelegramBotClient client, List<Command> _commands);       
    }
}

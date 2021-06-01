using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Clients;

namespace TelegramBot.Command.Commands
{
    class Start : Command
    {
        private readonly CovidClient _covidClient = new CovidClient();
        public override TelegramBotClient _client { get; set; }
        public override string Name { get; set; } = "/start";

        public override async void Execute(Message message, TelegramBotClient client, List<Command> commands)
        {
            string startText =
                    $"Привет {message.From.FirstName} {message.From.LastName}. Этот бот позволяет увидеть статистику по коронавирусу от любой страны, со всего мира ну и ещё парочку занимательных вещей. Работает он на английском, посему вводите названия стран/континентов на нем же. " +
                    $"\nВот список комманд:" +
                    $"\n/bycountry - Статистика по стране." +
                    $"\n/bycontinent - Статистика по континентам." +
                    $"\n/byworld - Статистика по миру." +
                    $"\n/bynumber - Информация о заболевших относительно цифры." +
                    $"\n/gettop - Топ стран по кол-ву заболевших.";
            await client.SendTextMessageAsync(message.From.Id, startText);
        }
    }
}

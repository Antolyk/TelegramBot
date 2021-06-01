using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramBot.Command.Commands;
using TelegramBot.Command;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient client;
        private static List<Command.Command> commands;
        static void Main(string[] args)
        {
            client = new TelegramBotClient("1788393158:AAGK7-K5CILRQuRsVofgi15vSI3wy-ZWIrE");
            client.OnMessage += BotOnMessageReceived;
            client.OnCallbackQuery += BotOnCallbackQueryReceived;
            commands = new List<Command.Command>();
            commands.Add(new GetStatisticByCountry());
            commands.Add(new GetStatisticByWorld());
            commands.Add(new GetAnswerByNumber());
            commands.Add(new Start());
            commands.Add(new GetTop10());
            commands.Add(new GetStatisticByContinent());
            var me = client.GetMeAsync().Result;
            Console.WriteLine(me.FirstName);
            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
        }

        private static void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.Text)
                return;
            string name = $"{message.From.FirstName} {message.From.LastName}";
            Console.WriteLine($"{name} отправил сообщение: '{message.Text}'");

           foreach(var comm in commands)
            {
                if(message.Text == comm.Name)
                {
                    comm.Execute(message, client, commands);
                }
            }
        }
    }
}

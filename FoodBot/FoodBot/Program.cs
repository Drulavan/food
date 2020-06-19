using FoodBot.Conversations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Telegram.Bot;

namespace FoodBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //собираем беседы в контейнер
            var collection = new ServiceCollection();
            Assembly ConsoleAppAssembly = typeof(Program).Assembly;

            var ConsoleAppTypes =
                from type in ConsoleAppAssembly.GetTypes()
                where !type.IsAbstract
                where typeof(IConversation).IsAssignableFrom(type)
                select type;

            foreach (var type in ConsoleAppTypes)
            {
                collection.AddTransient(typeof(IConversation), type);
            }
            var client = new TelegramBotClient("988895019:AAEJjB5p3dgnoDG9VTASPLABE-jysVPilxI");

            collection.AddSingleton(client);
            var serviceProvider = collection.BuildServiceProvider();

            var me = client.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello! I am user {me.Id} and my name is {me.FirstName}."
            );

            BotEngine engine = new BotEngine(serviceProvider.GetServices<IConversation>());
            client.OnMessage += engine.BotOnMessageReceived;
            client.OnReceiveError += engine.BotOnReceiveError;
            client.OnInlineQuery += engine.BotOnInlineQuery;
            client.OnCallbackQuery += engine.BotOnCallbackQuery;

            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
        }
    }
}
using FoodBot.Conversations;
using FoodBot.Parsers.Jobs;
using FoodBot.States;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FoodBot
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

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
            var client = new TelegramBotClient(configuration["BotKey"]);
            collection.AddSingleton(client);
            collection.AddSingleton(new List<UserState>());
            collection.AddSingleton(configuration);
            collection.AddTransient<IJob, ParseVkJob>();
            var serviceProvider = collection.BuildServiceProvider();

            // on-start self-check
            var me = client.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello! I am user {me.Id} and my name is {me.FirstName}."
            );

            // регистрируем движок и вешаем события
            BotEngine engine = new BotEngine(serviceProvider.GetServices<IConversation>(), serviceProvider.GetService<List<UserState>>());
            client.OnMessage += engine.BotOnMessageReceived;
            client.OnReceiveError += engine.BotOnReceiveError;
            client.OnInlineQuery += engine.BotOnInlineQuery;
            client.OnCallbackQuery += engine.BotOnCallbackQuery;

            // endless background job
            Thread workerThread = new Thread( () =>
            {
                while (true)
                {
                    var jobs = serviceProvider.GetServices<IJob>().ToList();
                    foreach (IJob j in jobs)
                    {
                         j.Execute();
                    };
                    Thread.Sleep(120000);
                }
            });
            workerThread.Start();

            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
     
        }


       
    }
}

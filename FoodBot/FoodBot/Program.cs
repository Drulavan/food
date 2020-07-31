using Cyriller;
using FoodBot.Conversations;
using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using FoodBot.Parsers;
using FoodBot.Parsers.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Telegram.Bot;

namespace FoodBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile("food.json", true, true)
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

            var conf = new Dictionary<Categories, List<string>>();
            configuration.GetSection("Categories").Bind(conf);
            var cyrPhrase = new CyrPhrase(new CyrNounCollection(), new CyrAdjectiveCollection());
            var foodDictionary = new Dictionary<Categories, List<string>>();
            foreach (var cat in conf.Keys)
            {
                var l = new List<string>();
                foreach (var list in conf.Values)
                {
                    foreach (var food in list)
                    {
                        l.AddRange(cyrPhrase.Decline(food, Cyriller.Model.GetConditionsEnum.Similar).ToList());
                    }
                }
                foodDictionary.Add(cat, l);
            }

            cyrPhrase = null;

            var client = new TelegramBotClient(configuration["BotKey"]);
            collection.AddSingleton<BotEngine>();
            collection.AddSingleton(client);
            collection.AddSingleton(configuration);
            collection.AddTransient<IJob, ParseVkJob>();
            collection.AddTransient<StateRepository>();
            collection.AddTransient<NoticeRepository>();
            collection.AddTransient<VkParser>();
            collection.AddSingleton(foodDictionary);
            collection.AddTransient<Categorizer>();
            var serviceProvider = collection.BuildServiceProvider();

            // on-start self-check
            var me = client.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello! I am user {me.Id} and my name is {me.FirstName}."
            );

            // регистрируем движок и вешаем события
            BotEngine engine = serviceProvider.GetService<BotEngine>();
            client.OnMessage += engine.BotOnMessageReceived;
            client.OnReceiveError += engine.BotOnReceiveError;
            client.OnInlineQuery += engine.BotOnInlineQuery;
            client.OnCallbackQuery += engine.BotOnCallbackQuery;

            // endless background job
            Thread workerThread = new Thread(() =>
            {
                while (true)
                {
                    var jobs = serviceProvider.GetServices<IJob>().ToList();
                    foreach (IJob j in jobs)
                    {
                        j.Execute();
                    };
                    Thread.Sleep(int.Parse(configuration["JobSleepTimer"]));
                }
            });
            workerThread.Start();

            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
        }
    }
}
using Cyriller;
using FoodBot.Conversations;
using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using FoodBot.Parsers;
using FoodBot.Parsers.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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
#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
        private static void Main(string[] args)
#pragma warning restore IDE0060 // Удалите неиспользуемый параметр
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\foodbot.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile("food.json", true, true)
               .AddJsonFile("vk.json", true, true)
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
            foreach (KeyValuePair<Categories, List<string>> cat in conf)
            {
                var l = new List<string>();
                
                foreach (var food in cat.Value)
                {
                    var s = cyrPhrase.Decline(food.ToLower(), Cyriller.Model.GetConditionsEnum.Similar).ToList();
                    var p = cyrPhrase.DeclinePlural(food.ToLower(), Cyriller.Model.GetConditionsEnum.Similar).ToList();
                    l.AddRange(s.Union(p).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList());
                }

                foodDictionary.Add(cat.Key, l);
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
            collection.AddSingleton<ILogger>(logger);
            collection.AddTransient<Categorizer>();
            collection.AddTransient<Geocoding>();
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
            Thread workerThread = new Thread(async () =>
            {
                while (true)
                {
                    var jobs = serviceProvider.GetServices<IJob>().ToList();
                    foreach (IJob j in jobs)
                    {
                        try
                        {
                            await j.Execute();
                        }
                        catch(Exception ex)
                        {
                            logger.Error("{@ex}", ex);
                        }
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
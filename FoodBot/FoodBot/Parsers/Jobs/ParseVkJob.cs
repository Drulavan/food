using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FoodBot.Parsers.Jobs
{
    /// <summary>
    /// Классы job реализуют паттерн стратегия позволяя по разному обрабатывать объявления из разных источников
    /// </summary>
    public class ParseVkJob : BaseJob, IJob
    {
        private readonly VkParser parser;

        private readonly NoticeRepository noticeRepository;

        public ParseVkJob(IConfiguration configuration, TelegramBotClient client, StateRepository stateRepository, NoticeRepository noticeRepository, VkParser parser)
            : base(configuration, client, stateRepository)
        {
            this.parser = parser;
            this.noticeRepository = noticeRepository;
        }

        public async Task Execute()
        {
            /// настроено рандомно для презентации
            /// в продакшн пойдет алгоритм исключающий аллергии и радиус через подсчет long/lat
            var random = new Random();
            var notices = await parser.GetNotices();

            var n = notices.ToList()[random.Next(notices.Count())];
            n.IsShown = true;
            noticeRepository.Add(n);

            // отправляем сообщение
            await SendNoticeAsync(n);
        }
    }
}
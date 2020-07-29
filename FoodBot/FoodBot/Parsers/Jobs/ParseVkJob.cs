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

        public ParseVkJob( TelegramBotClient client, StateRepository stateRepository, NoticeRepository noticeRepository, VkParser parser)
            : base(client, stateRepository)
        {
            this.parser = parser;
            this.noticeRepository = noticeRepository;
        }

        public async Task Execute()
        {
            var notices = await parser.GetNotices();

            //берем самую старую запись, но не старше суток
            var n = notices.Where(x=>x.Date>DateTime.Now.AddDays(-1))
                .OrderBy(x=>x.Date)
                .FirstOrDefault();

            if (n != null)
            {
                n.IsShown = true;
                noticeRepository.Add(n);

                // отправляем сообщение
                await SendNoticeAsync(n);
            }
        }
    }
}
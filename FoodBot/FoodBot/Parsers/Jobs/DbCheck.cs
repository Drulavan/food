using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FoodBot.Parsers.Jobs
{
    public class DbCheck :BaseJob, IJob
    {
        NoticeRepository noticeRepository;
        public DbCheck(IConfiguration configuration, TelegramBotClient client, List<UserState> states, NoticeRepository noticeRepository, ITextToSpeech textToSpeech) 
            : base(configuration, client, states, textToSpeech)
        {
            this.noticeRepository = noticeRepository;
        }

        public async Task Execute()
        {
            var n = noticeRepository.GetNew();
            if (n==null)
            {
                return;
            }
            n.IsShown = true;
            noticeRepository.Update(n);
            await SendNoticeAsync(n);
        }
    }
}

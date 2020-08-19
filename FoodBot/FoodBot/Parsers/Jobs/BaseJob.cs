using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Serilog;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Parsers.Jobs
{
    public abstract class BaseJob
    {
        private readonly TelegramBotClient client;
        private readonly StateRepository stateRepository;
        protected readonly ILogger Logger;

        public BaseJob(TelegramBotClient client, StateRepository stateRepository, ILogger logger)
        {
            this.stateRepository = stateRepository;
            this.client = client;
            Logger = logger;
        }

        public virtual async Task SendNoticeAsync(Notice n)
        {
            // Эта кнопка - наша метрика CTR. Предполагается что работать будет на редиректе типа http://metrics/post?param1=metric&param2=postUrl
            var inlineKeyboard = new InlineKeyboardMarkup(new InlineKeyboardButton()
            {
                Text = "Ссылка на пост",
                Url = n.Url
            });

            string caption = $"{n.FullText}";

            using var defaultPhoto = File.OpenRead(".\\Resources\\boxes_food.png");
            var photo = n.PhotosUrl.Where(x => !string.IsNullOrEmpty(x)).ToList().Count > 0 ?
                   new Telegram.Bot.Types.InputFiles.InputOnlineFile(n.PhotosUrl[0]) : new Telegram.Bot.Types.InputFiles.InputOnlineFile(defaultPhoto);

            var users = stateRepository.GetAll();
            foreach (var user in users)
            {
                await client.SendPhotoAsync(user.Id, photo, caption: caption, replyMarkup: inlineKeyboard);
            }

            Logger.Information("Message {id} sent to {@users}", n.Id , users);
        }
    }
}
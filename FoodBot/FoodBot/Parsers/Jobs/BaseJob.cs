using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Serilog;
using System;
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

            var users = stateRepository.GetAll().Where(
                x => x.IsRegistered
                && x.menuCat.Any(x=>n.Categories.Select(x=>x.DescriptionAttr()).Contains(x))
                && GetDistance(n, x) <= x.RadiusFind
                ).ToList();
            foreach (var user in users)
            {
                await client.SendPhotoAsync(user.Id, photo, caption: caption, replyMarkup: inlineKeyboard);
            }

            Logger.Information("Message {id} sent to {@users}", n.Id , users);
        }

        private double GetDistance(Notice n, UserState u)
        {
            // The radius of the earth in Km.
            // You could also use a better estimation of the radius of the earth
            // using decimals digits, but you have to change then the int to double.
            int R = 6371;

            double f1 = ConvertToRadians(n.Latitude);
            double f2 = ConvertToRadians(u.UsrLatitude);

            double df = ConvertToRadians(n.Latitude - u.UsrLatitude);
            double dl = ConvertToRadians(n.Longitude - u.UsrLongitude);

            double a = Math.Sin(df / 2) * Math.Sin(df / 2) +
            Math.Cos(f1) * Math.Cos(f2) *
            Math.Sin(dl / 2) * Math.Sin(dl / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate the distance.
            double d = R * c;

            return d;
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
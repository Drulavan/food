using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Parsers.Jobs
{
    public abstract class BaseJob
    {
        private readonly TelegramBotClient client;
        private readonly StateRepository stateRepository;
        private readonly OCR ocr;

        public BaseJob(IConfiguration configuration, TelegramBotClient client, StateRepository stateRepository)
        {
            ocr = new OCR(configuration);
            this.stateRepository = stateRepository;
            this.client = client;
        }

        public virtual async System.Threading.Tasks.Task SendNoticeAsync(Notice n)
        {
            // Эта кнопка - наша метрика CTR. Предполагается что работать будет на редиректе типа http://metrics/post?param1=metric&param2=postUrl
            var inlineKeyboard = new InlineKeyboardMarkup(new InlineKeyboardButton()
            {
                Text = "Ссылка на пост",
                Url = n.Url
            });

            string imageText = string.Empty;

            // здесь запрашиваем текст с картинки если нужен
            if (n.PhotosUrl.Count > 0 && string.IsNullOrEmpty(n.FullText))
            {
                imageText = await ocr.GetImageTextAsync(n.PhotosUrl[0]);
            }

            string caption = !String.IsNullOrEmpty(n.FullText) ? $"{n.FullText}" : $"{imageText}";

            using var defaultPhoto = File.OpenRead(".\\Resources\\boxes_food.png");

            foreach (var state in stateRepository.GetAll())
            {
                var photo = n.PhotosUrl.Count > 0 ?
                    new Telegram.Bot.Types.InputFiles.InputOnlineFile(n.PhotosUrl[0]) : new Telegram.Bot.Types.InputFiles.InputOnlineFile(defaultPhoto);
                await client.SendPhotoAsync(state.Id, photo, caption: caption, replyMarkup: inlineKeyboard);
            }
        }
    }
}
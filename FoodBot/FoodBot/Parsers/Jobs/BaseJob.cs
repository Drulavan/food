using FoodBot.Dal.Models;
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
        private readonly List<UserState> states;

        private readonly OCR ocr;

        public BaseJob(IConfiguration configuration, TelegramBotClient client, List<UserState> states)
        {
            ocr = new OCR(configuration);
            this.states = states;
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
            // здесь запрашиваем текст с картинки
            if (n.PhotosUrl.Count > 0)
            {
                imageText = await ocr.GetImageTextAsync(n.PhotosUrl[0]);
            }

            string caption = !String.IsNullOrEmpty(n.FullText) ? $"{n.FullText}" : $"{imageText}";
            // здесь идем в Amazon превратить текст в аудио

            using var fileStream = File.Create(@$"\bot\{n.Id}.ogg");
            using var defaultPhoto = File.OpenRead(".\\Resources\\boxes_food.png");

            foreach (var state in states)
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                var photo = n.PhotosUrl.Count > 0 ?
                    new Telegram.Bot.Types.InputFiles.InputOnlineFile(n.PhotosUrl[0]) : new Telegram.Bot.Types.InputFiles.InputOnlineFile(defaultPhoto);
                await client.SendPhotoAsync(state.Id, photo, caption: caption, replyMarkup: inlineKeyboard);
                await client.SendAudioAsync(state.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileStream), title: "Фудшеринг", performer: "Фудшеринг");
            }
            fileStream.Flush();
            fileStream.Close();
        }
    }
}
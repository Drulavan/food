using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using FoodBot.Dal.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Parsers.Jobs
{
    /// <summary>
    /// Классы job реализуют паттерн стратегия позволяя по разному обрабатывать объявления из разных источников
    /// </summary>
    public class ParseVkJob : IJob
    {
        private VkParser parser;
        private TelegramBotClient client;
        private List<UserState> states;
        private AmazonPollyClient awspc;
        private OCR ocr;

        public ParseVkJob(IConfiguration configuration, TelegramBotClient client, List<UserState> states)
        {
            parser = new VkParser(configuration);
            ocr = new OCR(configuration);
            this.states = states;
            this.client = client;
            awspc = new AmazonPollyClient(configuration["AWSID"], configuration["AWSAccessKey"], RegionEndpoint.USEast2);
        }

        public async Task Execute()
        {
            var random = new Random();
            var notices = await parser.GetNotices();
            var n = notices.ToList()[random.Next(notices.Count())];

            CtrMetric metric = new CtrMetric()
            {
                Source = n.Source,
                PostId = n.Id.ToString(),
            };
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
            SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest
            {
                Text = $"{caption}",
                OutputFormat = OutputFormat.Ogg_vorbis,
                SampleRate = "16000",
                VoiceId = VoiceId.Tatyana
            };
            SynthesizeSpeechResponse sres = await awspc.SynthesizeSpeechAsync(sreq);

            using (var fileStream = File.Create(@$"\bot\{n.Id}.ogg"))
            {
                using var defaultPhoto = File.OpenRead(".\\Resources\\boxes_food.png");
                sres.AudioStream.CopyTo(fileStream);
              
                foreach (var state in states)
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    var photo = n.PhotosUrl.Count > 0 ?
                        new Telegram.Bot.Types.InputFiles.InputOnlineFile(n.PhotosUrl[0]) : new Telegram.Bot.Types.InputFiles.InputOnlineFile(defaultPhoto);
                    await client.SendPhotoAsync(state.Id, photo, caption: caption, replyMarkup: inlineKeyboard);
                    await client.SendAudioAsync(state.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileStream), title: "Фудшеринг",performer: "Фудшеринг");
                }
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
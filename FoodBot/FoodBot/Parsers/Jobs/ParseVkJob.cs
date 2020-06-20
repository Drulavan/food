using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using FoodBot.Conversations;
using FoodBot.Helpers;
using FoodBot.States;
using Microsoft.Extensions.Configuration;
using OpusDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Parsers.Jobs
{

    public class ParseVkJob : IJob
    {
        VkParser parser;
        TelegramBotClient client;
        List<UserState> states;
        AmazonPollyClient awspc;
        public ParseVkJob(IConfiguration configuration, TelegramBotClient client, List<UserState> states)
        {
            parser = new VkParser(configuration);
            this.states = states;
            this.client = client;
            awspc = new AmazonPollyClient(configuration["AWSID"], configuration["AWSAccessKey"], RegionEndpoint.USEast2);
        }

        public async Task Execute()
        {
            var random = new Random();
            var notices = await parser.GetNotices();
            var n = notices.ToList()[random.Next(notices.Count())];
            var inlineKeyboard = new InlineKeyboardMarkup(new InlineKeyboardButton()
            {
                Text = "Ссылка на пост",
                Url = n.Url
            });

           
            SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest
            {
                Text = $"{n.FullText}",
                OutputFormat = OutputFormat.Ogg_vorbis,
                SampleRate = "16000",
                VoiceId = VoiceId.Tatyana
            };
            SynthesizeSpeechResponse sres = await awspc.SynthesizeSpeechAsync(sreq);

            using (var fileStream = File.Create(@$"\bot\{n.Id}.ogg"))
            {
                sres.AudioStream.CopyTo(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);
                foreach (var state in states)
                {
                    await client.SendTextMessageAsync(state.Id, $"{n.FullText}", replyMarkup: inlineKeyboard);
                    await client.SendAudioAsync(state.Id,new Telegram.Bot.Types.InputFiles.InputOnlineFile(fileStream));
                }
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }


   
}

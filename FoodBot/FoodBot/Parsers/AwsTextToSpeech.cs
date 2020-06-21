using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FoodBot.Parsers
{
    public class AwsTextToSpeech : ITextToSpeech
    {
        private AmazonPollyClient awspc;
        public AwsTextToSpeech(IConfiguration configuration)
        {
            awspc = new AmazonPollyClient(configuration["AWSID"], configuration["AWSAccessKey"], RegionEndpoint.USEast2);
        }

        public async Task<SynthesizeSpeechResponse> TextToSpeechAsync(string text)
        {
            SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest
            {
                Text = $"{text}",
                OutputFormat = OutputFormat.Ogg_vorbis,
                SampleRate = "16000",
                VoiceId = VoiceId.Tatyana
            };
            return await awspc.SynthesizeSpeechAsync(sreq);
           
        }
    }
}

using Amazon.Polly.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FoodBot.Parsers
{
    public interface ITextToSpeech
    {
        public Task<SynthesizeSpeechResponse> TextToSpeechAsync(string text);
    }
}

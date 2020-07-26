using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodBot.Parsers
{
    public class OCR
    {
        private readonly string apiKey;

        public OCR(IConfiguration configuration)
        {
            apiKey = configuration["OCRKey"];
        }

        public async Task<string> GetImageTextAsync(string ImageUrl)
        {
            string text = "";
            try
            {
                HttpClient httpClient = new HttpClient
                {
                    Timeout = new TimeSpan(1, 1, 1)
                };

                MultipartFormDataContent form = new MultipartFormDataContent
                {
                    { new StringContent(apiKey), "apikey" }, //Added api key in form data
                    { new StringContent("rus"), "language" },
                    { new StringContent(ImageUrl), "url" }
                };

                HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);

                string strContent = await response.Content.ReadAsStringAsync();
                Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);

                if (ocrResult.OCRExitCode == 1)
                {
                    for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                    {
                        text += ocrResult.ParsedResults[i].ParsedText;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }

            return text;
        }
    }

    public class Rootobject
    {
        public Parsedresult[] ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class Parsedresult
    {
        public object FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
}
using FoodBot.Dal.Repositories;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FoodBot.Parsers.Jobs
{
    /// <summary>
    /// Классы job реализуют паттерн стратегия позволяя по разному обрабатывать объявления из разных источников
    /// </summary>
    public class ParseVkJob : BaseJob, IJob
    {
        private readonly VkParser parser;
        private readonly NoticeRepository noticeRepository;
        private readonly Categorizer categorizer;
        private readonly Geocoding geocoding;

        public ParseVkJob(
            TelegramBotClient client,
            StateRepository stateRepository,
            NoticeRepository noticeRepository,
            VkParser parser,
            Categorizer categorizer,
            Geocoding geocoding,
            ILogger logger)
            : base(client, stateRepository, logger)
        {
            this.parser = parser;
            this.noticeRepository = noticeRepository;
            this.categorizer = categorizer;
            this.geocoding = geocoding;
        }

        public async Task Execute()
        {
            var notices = await parser.GetNotices();

            //берем самую старую запись, но не старше суток
            var n = notices.Where(x => x.Date > DateTime.Now.AddDays(-1))
                .OrderBy(x => x.Date)
                .FirstOrDefault();

            if (n != null)
            {
                n.Categories = categorizer.Categorize(n.FullText);
                var coordinates = await geocoding.GetCoordinatesAsync(n.FullText.RemovePunctuation());

                if ((bool)coordinates?.Results.Any())
                {
                    n.Latitude = coordinates.Results.FirstOrDefault().Geometry.Location.Latitude;
                    n.Longitude = coordinates.Results.FirstOrDefault().Geometry.Location.Longitude;
                }
            
                n.IsShown = true;
                Logger.Information("Add notice {@n}", n);
                noticeRepository.Add(n);

                // отправляем сообщение
                await SendNoticeAsync(n);
            }
        }
    }
}
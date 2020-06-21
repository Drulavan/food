using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodBotAPI.Controllers
{
    [ApiController]
    [Route("food")]
    public class ShareFoodController : ControllerBase
    {
        private readonly ILogger<ShareFoodController> _logger;
        private readonly NoticeRepository _noticeRepository;

        public ShareFoodController(ILogger<ShareFoodController> logger, NoticeRepository noticeRepository)
        {
            _logger = logger;
            _noticeRepository = noticeRepository;
        }

        /// <summary>
        /// Получает список последних постов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Notice> Get(int n = 10)
        {
            try
            {
                return _noticeRepository.GetAll().OrderByDescending(x => x.Date).Take(n).ToList();
            }
            catch
            {
                return new List<Notice>();
            }
            
        }

        /// <summary>
        /// Публикация объявления
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Post(Notice notice)
        {
            try
            {
                 _noticeRepository.Add(notice);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

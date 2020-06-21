using FoodBot.Dal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodBot.Dal.Repositories
{
    public class NoticeRepository : BaseRepository<Notice>
    {
        public NoticeRepository() : base("Notices")
        {
        }
    }
}

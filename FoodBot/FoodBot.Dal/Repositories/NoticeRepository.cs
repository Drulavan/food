using FoodBot.Dal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FoodBot.Dal.Repositories
{
    public class NoticeRepository : BaseRepository<Notice>
    {
        public NoticeRepository() : base("Notices")
        {
        }
        public Notice GetNew()
        {

            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<Notice>("Notices");
                var result = col.FindAll().Where(x => x.IsShown == false && x.Source==1).FirstOrDefault();
                return result;
            }


        }
        public bool Exists(long id)
        {

            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<Notice>("Notices");
                var result = col.Exists(x => x.Id==id);
                return result;
            }


        }
    }
}

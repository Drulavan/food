using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FoodBot.Dal.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected const string DBNAME = @"Filename=\bot\FoodBot.db; Connection=shared";
        private readonly string tableName;

        protected BaseRepository(string tableName)
        {
            this.tableName = tableName;
        }

        public void Add(T entity)
        {
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                col.Insert(entity);
            }
        }

        public void Update(T entity)
        {
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                col.Update(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> results;
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                return results = col.FindAll().ToList();
            }
        }

     

    }
}

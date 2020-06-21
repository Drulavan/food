using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodBot.Dal.Repositories
{
    public abstract class BaseRepository<T>
    {
        const string DBNAME = "FoodBot.db";
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

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> results;
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                return results = col.FindAll().ToList();
            }
        }

        public T Get(long id)
        {

            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                return col.FindById(id);
            }


        }

        public bool IsExist(long id)
        {

            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                return col.Exists();
            }


        }
    }
}

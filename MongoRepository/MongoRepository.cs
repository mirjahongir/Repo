using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoRepository
{
    public class MongoRepository<T> : IRepository<T, string>
        where T : class, IDomain<string>
    {
        public IMongoDatabase Database { get { return _data; } }
        IMongoDatabase _data;
        public IMongoCollection<T> Collection { get { return _db; } }
        public IMongoCollection<T> _db;
        public MongoRepository(IMongoDatabase data) : this(data, typeof(T).Name.ToLower())
        {
        }
        public MongoRepository(IMongoDatabase data, string collectionName)
        {
            _data = data;
            _db = data.GetCollection<T>(collectionName);
        }
        public void Add(T model)
        {
            _db.InsertOne(model);
        }

        public void AddRange(IEnumerable<T> models)
        {
            _db.InsertMany(models);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _db.AsQueryable<T>().Where(predicate);
        }

        public T Get(string id)
        {
            return _db.Find(id).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return Find(m => true);
        }

        public void Remove(T model)
        {
            _db.DeleteOne(m => m.Id == model.Id);
        }

        public void RemoveRange(IEnumerable<T> models)
        {
            foreach (var i in models)
            {
                _db.DeleteOne(m => m.Id == i.Id);
            }

        }
    }
}

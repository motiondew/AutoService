using assignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace assignment2.Data
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IMongoObject
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(DatabaseSettings settings)
        {
            //Don't worry too much about opening and closing connections. 
            //The MongoDB C# driver maintains an internal connection pool, 
            //so you won't suffer overheads of opening and closing actual 
            //connections each time you create a new MongoServer object.

            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.Database);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        public IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            _collection.DeleteOne<T>(s => s.BsonID == id);
        }

        public IEnumerable<T> Get()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public T GetById(string id)
        {
            T entity = _collection.Find<T>(s => s.BsonID == id).FirstOrDefault();
            return entity;
        }

        public T Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _collection.ReplaceOne<T>(s=> s.BsonID == entity.BsonID,entity);
        }

        private protected string GetCollectionName(Type classType)
        {
            return ((BsonCollectionAttribute)classType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
        public bool DuplicateBsonId(T doc)
        {
            return _collection.Find(new BsonDocument()).ToList().Any(c => c.BsonID == doc.BsonID);
        }

    }
}

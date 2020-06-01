using assignment2.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace assignment2.Data
{
    public interface IMongoRepository<T> where T : IMongoObject
    {
        IEnumerable<T> Get();
        T GetById(string id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(string id);
        T Retrieve(int id);
        bool DuplicateBsonId(T doc);
        IQueryable<T> AsQueryable();
    }
}

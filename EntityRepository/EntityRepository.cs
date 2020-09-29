using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repo;
namespace EntityRepository
{
    public class EntityRepository<T, TKey> : IRepository<T, TKey>
         where T : class
    {
        public  DbContext Context { get { return _context; } }
        private readonly DbContext _context;

        public DbSet<T> _db;
        public DbSet<T> Db { get { return _db; } }
        public EntityRepository(DbContext context)
        {
            _context = context;
            _db = _context.Set<T>();

        }
        public void Add(T model)
        {
            _db.Add(model);
            _context.SaveChanges();
        }
        public IQueryable<T> Query(string sql,  params object[] par)
        {
          
            var result=_db.FromSqlRaw(sql,par);
            return result;
        }
        public void AddRange(IEnumerable<T> models)
        {
            _db.AddRange(models);
            _context.SaveChanges();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
           return _db.Where(predicate);
        }

        public T Get(TKey id)
        {
           return _db.Find(id);
        }

        public IQueryable<T> GetAll()
        {
           return _db.Where(m => true);
        }

        public void Remove(T model)
        {
            _db.Remove(model);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> models)
        {
            _db.RemoveRange(models);
            _context.SaveChanges();
        }
    }

}

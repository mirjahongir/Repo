using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repo;
namespace EntityRepository
{
    public class EntityRepository<T, TKey> : IRepository<T, TKey>
         where T : class, IDomain<TKey>
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
        public virtual void Add(T model)
        {
            _db.Add(model);
            _context.SaveChanges();
        }
        public virtual IQueryable<T> Query(string sql,  params object[] par)
        {
          
            var result=_db.FromSqlRaw(sql,par);
            return result;
        }
        public virtual void AddRange(IEnumerable<T> models)
        {
            _db.AddRange(models);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
           return _db.Where(predicate);
        }

        public virtual T Get(TKey id)
        {
           return _db.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
           return _db.Where(m => true);
        }

        public virtual void Remove(T model)
        {
            _db.Remove(model);
            _context.SaveChanges();
        }

        public virtual void RemoveRange(IEnumerable<T> models)
        {
            _db.RemoveRange(models);
            _context.SaveChanges();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using Data.Interfaces;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity Entity)
        {
            Context.Set<TEntity>().Add(Entity);
        }

        public void AddRange(IEnumerable<TEntity> Entities)
        {
            Context.Set<TEntity>().AddRange(Entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> Predicate)
        {
            return Context.Set<TEntity>().Where(Predicate);
        }

        public TEntity Get(int Id)
        {
            return Context.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity Entity)
        {
            Context.Set<TEntity>().Remove(Entity);
        }

        public void RemoveRange(IEnumerable<TEntity> Entites)
        {
            Context.Set<TEntity>().RemoveRange(Entites);
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

    }
}
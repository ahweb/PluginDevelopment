using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace PluginDevelopment.DAL.EF.DAL
{
    public class BaseDal<T> where T : class, new()
    {
        private readonly DbContext _dbContext = DbContextFactory.Create();

        public void Add(T t)
        {
            _dbContext.Set<T>().Add(t);
        }

        public void Delete(T t)
        {
            _dbContext.Set<T>().Remove(t);
        }

        public void Update(T t)
        {
            _dbContext.Set<T>().AddOrUpdate(t);
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return _dbContext.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> GetMoelsByPage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, TType>> orderByLambda, Expression<Func<T, bool>> whereLambda)
        {
            //是否升序
            if (isAsc)
            {
                return _dbContext.Set<T>().Where(whereLambda).OrderBy(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return _dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public bool SaveChange()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}


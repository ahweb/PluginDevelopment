using System;
using System.Linq;
using System.Linq.Expressions;

namespace PluginDevelopment.BLL
{
    public interface IBaseService<T> where T : class, new()
    {
        bool Add(T t);

        bool Delete(T t);

        bool Update(T t);

        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetModelsByPage<TType>(int pageSize, int pageIndex, bool isAsc, 
            Expression<Func<T, TType>> orderByLambda, Expression<Func<T, bool>> whereLambda);
    }
}

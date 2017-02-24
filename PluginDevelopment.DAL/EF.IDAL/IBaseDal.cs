using System;
using System.Linq;
using System.Linq.Expressions;

namespace PluginDevelopment.DAL.EF.IDAL
{
    public interface IBaseDal<T> where T:class ,new()
    {
        void Add(T t);

        void Delete(T t);

        void Update(T t);

        IQueryable<T> GetModels(Expression<Func<T,bool>> whereLambda );

        IQueryable<T> GetMoelsByPage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, TType>> orderByLambda, Expression<Func<T, bool>> whereLambda);

        bool SaveChange();
    }
}

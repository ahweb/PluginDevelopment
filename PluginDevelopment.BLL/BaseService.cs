using System;
using System.Linq;
using System.Linq.Expressions;
using PluginDevelopment.DAL.EF.DAL;
using PluginDevelopment.DAL.EF.IDAL;

namespace PluginDevelopment.BLL
{
    public abstract  class BaseService<T> where T : class, new()
    {
        public BaseService()
        {
            SetDal();
        }

        public IBaseDal<T> Dal { get; set; }

        public abstract void SetDal();

        public bool Add(T t)
        {
            Dal.Add(t);
            return Dal.SaveChange();
        }
        public bool Delete(T t)
        {
            Dal.Delete(t);
            return Dal.SaveChange();
        }
        public bool Update(T t)
        {
            Dal.Update(t);
            return Dal.SaveChange();
        }
        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.GetModels(whereLambda);
        }

        public IQueryable<T> GetModelsByPage<TType>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, TType>> orderByLambda, Expression<Func<T, bool>> whereLambda)
        {
            return Dal.GetMoelsByPage(pageSize, pageIndex, isAsc, orderByLambda, whereLambda);
        }
    }
}

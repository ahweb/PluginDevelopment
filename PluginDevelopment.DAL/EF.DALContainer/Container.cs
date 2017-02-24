using System;
using Autofac;
using PluginDevelopment.DAL.EF.DAL;
using PluginDevelopment.DAL.EF.IDAL;

namespace PluginDevelopment.DAL.EF.DALContainer
{
    public class Container
    {
        /// <summary>
        /// IOC 容器
        /// </summary>
        private static IContainer _container;

        /// <summary>
        /// 获取 IDal 的实例化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                if (_container == null)
                {
                    Initialise();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("IOC实例化出错!" + ex.Message);
            }

            return _container.Resolve<T>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialise()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserDal>().As<IUserDal>().InstancePerLifetimeScope();
            _container = builder.Build();
        }
    }
}

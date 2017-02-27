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
            //为了注册组件而创建ContainerBuilder对象
            var builder = new ContainerBuilder();

            //注册一个类型并将其所实现的接口暴露为服务，使用RegisterType必须有一个具体的类型
            //可以将一个抽象类或者接口暴露为服务，但却不能将抽象类或者接口注册为组件。
            //InstancePerLifetimeScope配置组件，使每个依赖组件或调用Resolve（）
            //在单个ILifetimeScope中获得相同的共享实例。 依赖组件
            //在不同的生命周期范围内会得到不同的实例。
            builder.RegisterType<UserDal>().As<IUserDal>().InstancePerLifetimeScope();

            //创建容器来完成注册工作
            _container = builder.Build();

        }
    }
}

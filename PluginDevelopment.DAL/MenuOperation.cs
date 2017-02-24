using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using PluginDevelopment.Helper;
using PluginDevelopment.Helper.Dapper.NET;
using PluginDevelopment.Model;

namespace PluginDevelopment.DAL
{
    public class MenuOperation
    {
        private static readonly DbBase Dbbase = new DbBase("DefaultConnectionString");

        /// <summary>
        /// 获取树形文档列表
        /// </summary>
        /// <returns></returns>
        public static string GetMenuDocument()
        {
            var sqlStr = "select a.id,a.name,a.parentid,a.url,a.icon from menu a group by a.sort";
            IList<Menu> menuList;
            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                menuList = conDbconnection.Query<Menu>(sqlStr).ToList();
            }
            //获取集合中父ID为空的记录
            var parentMenus = menuList.Where(x => string.IsNullOrEmpty(x.ParentId)).ToList();
            //循环遍历父ID为空的集合
            foreach (var menu in parentMenus)
            {
                //递归获取父ID为空的子节点
                FeatchMenuChildren(menuList, menu);
            }
            return JsonConvert.SerializeObject(parentMenus);
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="menus">查询得到的菜单集合</param>
        /// <param name="menu">父节点为空的集合</param>
        public static void FeatchMenuChildren(IList<Menu> menus, Menu menu)
        {
            menu.Children = new Collection<Menu>(menus.Where(x => x.ParentId.Equals(menu.Id)).ToList());
            if (!menu.Children.Any()) return;
            foreach (var childMenu in menu.Children)
            {
                FeatchMenuChildren(menus, childMenu);
            }
        }
    }
}

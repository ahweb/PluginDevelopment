using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperForNet;
using Newtonsoft.Json;
using PluginDevelopment.Model;

namespace PluginDevelopment.DAL
{
   public class TreeDocumentOperation
    {
     private static readonly DbBase Dbbase = new DbBase("DefaultConnectionString");

        /// <summary>
        /// 获取树形文档列表
        /// </summary>
        /// <returns></returns>
        public static string GetTreeDocument()
       {
            var sqlStr = "select * from menu a group by a.sort";

            IList<Menu> menuList;

            var result = new List<object>();

            using (var conDbconnection = Dbbase.DbConnecttion)
            {
                menuList = conDbconnection.Query<Menu>(sqlStr).ToList();
            }

            //获取根节点
            foreach (var org in menuList.Where(x => string.IsNullOrEmpty(x.ParentId)))
            {
                result.Add(new
                {
                    Parent = 0,
                    Key = org.Id,
                    Name = org.Name,
                    Icon = org.Icon,
                    Url = org.Url
                });
                //根据根节点查询该根节点下的所有子节点
                FeatchMenuChildren(result, menuList, org);
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 递归填充菜单下的子菜单
        /// </summary>
        /// <param name="result">返回前台的菜单集合</param>
        /// <param name="menus">所有的菜单列表</param>
        /// <param name="menu">当前菜单</param>
        public static void FeatchMenuChildren(List<object> result, IList<Menu> menus, Menu menu)
        {
            //根据节点ID获取该查询子节点list集合
            var childrenMenus = menus.Where(x => x.ParentId.Equals(menu.Id));
            var childMenus = childrenMenus as Menu[] ?? childrenMenus.ToArray();
            if (!childMenus.Any()) return;
            foreach (var childMenu in childMenus)
            {
                result.Add(new
                {
                    Parent = menu.Id,
                    Key = childMenu.Id,
                    Name = childMenu.Name,
                    Icon = childMenu.Icon,
                    Url = childMenu.Url
                });
                FeatchMenuChildren(result, menus, childMenu);
            }
        }
    }
}

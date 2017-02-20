using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PluginDevelopment.DAL;

namespace PluginDevelopment.Controllers
{
    public class MenuController : ApiController
    {
        public string GetMenuData()
        {
            var menuData = MenuOperation.GetMenuDocument();
            return menuData;
        }
    }
}

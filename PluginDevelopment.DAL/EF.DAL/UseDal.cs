using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginDevelopment.DAL.EF.IDAL;
using PluginDevelopment.Model;

namespace PluginDevelopment.DAL.EF.DAL
{
    public partial class UserDal : BaseDal<user>,IUserDal
    {
    }
}

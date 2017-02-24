using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginDevelopment.Model;
using PluginDevelopment.DAL;
using PluginDevelopment.DAL.EF.IDAL;

namespace PluginDevelopment.BLL
{
    public  class UserService : BaseService<user>, IUserService
    {
        readonly IUserDal _userDal = Container.Resolve<IUserDal>();
        public override void SetDal()
        {
            Dal = _userDal;
        }
    }
}

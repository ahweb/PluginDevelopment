using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginDevelopment.Model;
using PluginDevelopment.DAL;
using PluginDevelopment.DAL.EF.IDAL;
using PluginDevelopment.DAL.EF.DALContainer;

namespace PluginDevelopment.BLL
{
    public  class UserService : BaseService<user>, IUserService
    {
        readonly IUserDal _userDal = DAL.EF.DALContainer.Container.Resolve<IUserDal>();

        public override void SetDal()
        {
            Dal = _userDal;
        }
    }
}

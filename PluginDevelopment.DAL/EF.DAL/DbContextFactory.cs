using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using PluginDevelopment.Model;

namespace PluginDevelopment.DAL.EF.DAL
{
   public class DbContextFactory
    {
       public static DbContext Create()
       {
           DbContext dbContext=CallContext.GetData("DbContext") as DbContext;

           if (dbContext != null)
           {
               return dbContext;
           }
           dbContext = new dotnetpluginEntities();
           CallContext.SetData("DbContext", dbContext);
           return dbContext;
        }
    }
}

using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.BLL;
using UsersAndAwards.BLL.Interfaces;
using UsersAndAwards.DAL;
using UsersAndAwards.DAL.Interfaces;

namespace UsersAndAwards.Ioc
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserDao>().To<UserDao>();
            Bind<IAwardDao>().To<AwardDao>();

            Bind<IUserLogic>().To<UserLogic>();
            Bind<IAwardLogic>().To<AwardLogic>();
        }
    }
}

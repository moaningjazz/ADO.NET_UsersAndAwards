using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAndAwards.Ioc
{
    public static class DependencyResolver
    {
        private static Bindings _bindings = new Bindings();
        public static StandardKernel Kernel = new StandardKernel(_bindings);
    }
}

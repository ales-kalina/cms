using System.Collections.Generic;
using Ninject;

namespace CMS.Core
{

    public sealed class ApplicationStartupInfo
    {

        public IEnumerable<IModule> Modules { get; private set; }
        public IKernel Kernel { get; private set; }

        internal ApplicationStartupInfo(IEnumerable<IModule> modules, IKernel kernel)
        {
            Modules = modules;
            Kernel = kernel;
        }

    }

}
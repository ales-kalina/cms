using System.Collections.Generic;

namespace CMS.Core
{

    public interface IModuleBootstrapper
    {

        void Run(IEnumerable<IModule> modules);

    }

}
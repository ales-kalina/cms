using System.Collections.Generic;

namespace CMS.Core
{

    public interface IModuleProvider
    {

        IEnumerable<IModule> GetModules();

    }

}
using System;
using System.Collections.Generic;

namespace CMS.Core
{

    public interface IModuleStatusManager
    {

        IEnumerable<IModule> GetInstalledModules(IEnumerable<IModule> modules);
        IEnumerable<IModule> GetUpdatedModules(IEnumerable<IModule> modules);
        void UpdateEntriesForModules(IEnumerable<IModule> modules);
        void RemoveEntriesForModules(IEnumerable<IModule> modules);

    }

}
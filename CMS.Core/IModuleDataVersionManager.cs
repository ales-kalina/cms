using System.Collections.Generic;

namespace CMS.Core
{

    public interface IModuleDataVersionManager
    {

        int GetModuleDataVersion(IModule module);
        void SetModuleDataVersion(IModule module, int version);
        void RemoveEntriesForModules(IEnumerable<IModule> modules);

    }

}
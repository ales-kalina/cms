using System;

namespace CMS.Core
{

    public interface IMigrationManager
    {
        
        int UpgradeModule(IModule module, int currentVersion);
    }

}
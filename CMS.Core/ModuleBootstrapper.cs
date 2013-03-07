using System.Collections.Generic;
using NLog;

namespace CMS.Core
{

    public sealed class ModuleBootstrapper : IModuleBootstrapper
    {

        private IModuleStatusManager mModuleStatusManager;
        private IModuleDataVersionManager mModuleDataVersionManager;
        private IMigrationManager mMigrationManager;

        private Logger mLog = LogManager.GetLogger("ModuleBootstrapper");

        public ModuleBootstrapper(IModuleStatusManager moduleStatusManager, IModuleDataVersionManager moduleDataVersionManager, IMigrationManager migrationManager)
        {
            mModuleStatusManager = moduleStatusManager;
            mModuleDataVersionManager = moduleDataVersionManager;
            mMigrationManager = migrationManager;
        }

        public void Run(IEnumerable<IModule> modules)
        {
            mLog.Debug("Start");
            Update(modules);
            Install(modules);
            mLog.Debug("End");
        }

        private void Install(IEnumerable<IModule> modules)
        {
            IEnumerable<IModule> candidates = mModuleStatusManager.GetInstalledModules(modules);
            foreach (IModule candidate in candidates)
            {
                mLog.Debug("Upgrading data of installed module {0}", candidate.Name);
                Upgrade(candidate);
                candidate.Install();
            }
            mModuleStatusManager.UpdateEntriesForModules(candidates);
        }

        private void Update(IEnumerable<IModule> modules)
        {
            IEnumerable<IModule> candidates = mModuleStatusManager.GetUpdatedModules(modules);
            foreach (IModule candidate in candidates)
            {
                mLog.Debug("Upgrading data of updated module {0}", candidate.Name);
                Upgrade(candidate);
                candidate.Update();
            }
            mModuleStatusManager.UpdateEntriesForModules(candidates);
        }

        private void Upgrade(IModule module)
        {
            int currentVersion = mModuleDataVersionManager.GetModuleDataVersion(module);
            int version = mMigrationManager.UpgradeModule(module, currentVersion);
            
            mModuleDataVersionManager.SetModuleDataVersion(module, version);
            mLog.Debug("Data of module {0} were upgraded from version {1} to version {2}", module.Name, currentVersion, version);
        }

    }

}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet;

namespace CMS.Core
{

    public abstract class ApplicationManagerBase : IApplicationManager
    {

        private IApplicationPackageManager mPackageManager;
        private IModuleStatusManager mModuleStatusManager;
        private IModuleDataVersionManager mModuleDataVersionManager;
        private IModuleProvider mModuleProvider;

        public ApplicationManagerBase(IApplicationPackageManager packageManager, IModuleStatusManager moduleStatusManager, IModuleDataVersionManager moduleDataVersionManager, IModuleProvider moduleProvider)
        {
            mPackageManager = packageManager;
            mModuleStatusManager = moduleStatusManager;
            mModuleDataVersionManager = moduleDataVersionManager;
            mModuleProvider = moduleProvider;
        }

        public IEnumerable<PackageManagerMessage> InstallPackage(IPackage package)
        {
            IEnumerable<PackageManagerMessage> messages = mPackageManager.InstallPackage(package);
            if (!messages.Any(x => x.Level == MessageLevel.Error))
            {
                RestartApplication();
            }

            return messages;
        }

        public IEnumerable<PackageManagerMessage> UpdatePackage(IPackage package)
        {
            IEnumerable<PackageManagerMessage> messages = mPackageManager.UpdatePackage(package);
            if (!messages.Any(x => x.Level == MessageLevel.Error))
            {
                RestartApplication();
            }

            return messages;
        }

        public IEnumerable<PackageManagerMessage> UninstallPackage(IPackage package)
        {
            List<IModule> modules = new List<IModule>();
            IEnumerable<IPackageFile> files = package.GetLibFiles().Where(x => x.TargetFramework == null || x.TargetFramework == mPackageManager.TargetFramework);
            foreach (IPackageFile file in files)
            {
                string assemblyName = Path.GetFileNameWithoutExtension(file.EffectivePath);
                IEnumerable<IModule> candidates = mModuleProvider.GetModules().Where(x => x.GetType().Assembly.GetName().Name == assemblyName);
                modules.AddRange(candidates);
            }

            foreach (IModule module in modules)
            {
                module.Remove();
            }
            mModuleStatusManager.RemoveEntriesForModules(modules);
            mModuleDataVersionManager.RemoveEntriesForModules(modules);

            IEnumerable<PackageManagerMessage> messages = mPackageManager.UninstallPackage(package);
            if (!messages.Any(x => x.Level == MessageLevel.Error))
            {
                RestartApplication();
            }

            return messages;
        }

        protected abstract void RestartApplication();

    }

}
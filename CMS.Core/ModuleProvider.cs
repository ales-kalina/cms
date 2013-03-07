using System.Collections.Generic;

namespace CMS.Core
{

    public sealed class ModuleProvider : IModuleProvider
    {

        private IEnumerable<IModule> mModules;

        public ModuleProvider(IEnumerable<IModule> modules)
        {
            mModules = modules;
        }

        public IEnumerable<IModule> GetModules()
        {
            return mModules;
        }

    }

}
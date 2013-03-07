using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace CMS.Core
{

    public abstract class ApplicationBootstrapperBase
    {

        public void Run()
        {
            NinjectSettings settings = new NinjectSettings
            {
                InjectNonPublic = false,
                InjectParentPrivateProperties = false,
                LoadExtensions = false,
                UseReflectionBasedInjection = true
            };
            Locator.Kernel = new StandardKernel(settings);
            IEnumerable<IModule> modules = GetModules();
            ApplicationStartupInfo startupInfo = new ApplicationStartupInfo(modules, Locator.Kernel);
            StartApplication(startupInfo);
        }

        abstract protected void StartApplication(ApplicationStartupInfo startupInfo);

        private IEnumerable<IModule> GetModules()
        {
            ModuleManager moduleManager = new ModuleManager();
            string[] patterns = new string[] { "*.dll" };
            moduleManager.LoadModules(patterns);
            
            return moduleManager.GetModules().ToArray();
        }

    }

}
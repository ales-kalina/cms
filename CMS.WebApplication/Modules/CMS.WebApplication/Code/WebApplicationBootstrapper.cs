using System.Collections.Generic;
using System.IO;
using System.Web;
using CMS.Core;
using Ninject;

namespace CMS.WebApplication
{

    public class WebApplicationBootstrapper : ApplicationBootstrapperBase
    {

        public static void Start()
        {
            WebApplicationBootstrapper bootstrapper = new WebApplicationBootstrapper();
            bootstrapper.Run();
        }

        protected override void StartApplication(ApplicationStartupInfo startupInfo)
        {
            RegisterSystemServices(startupInfo.Kernel, startupInfo.Modules);
            IModuleBootstrapper moduleBootstrapper = Locator.Get<IModuleBootstrapper>();
            moduleBootstrapper.Run(startupInfo.Modules);
            foreach (IModule module in startupInfo.Modules)
            {
                module.RegisterServices(startupInfo.Kernel);
            }
            foreach (IModule module in startupInfo.Modules)
            {
                module.Load();
            }
        }

        private void RegisterSystemServices(IKernel kernel, IEnumerable<IModule> modules)
        {
            kernel.Bind<IModuleStatusManager>().ToMethod(context => CreateModuleStatusManager()).InSingletonScope();
            kernel.Bind<IModuleDataVersionManager>().ToMethod(context => CreateModuleDataVersionManager()).InSingletonScope();
            kernel.Bind<IMigrationManager>().To<MigrationManager>().InSingletonScope();
            kernel.Bind<IModuleBootstrapper>().To<ModuleBootstrapper>().InSingletonScope();
            kernel.Bind<IApplicationPackageManager>().ToMethod(context => CreateApplicationPackageManager()).InSingletonScope();
            kernel.Bind<IModuleProvider>().ToMethod(context => new ModuleProvider(modules)).InSingletonScope();
            kernel.Bind<IApplicationManager>().To<WebApplicationManager>().InSingletonScope();
        }

        private IModuleStatusManager CreateModuleStatusManager()
        {
            string storeFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, "module-status.xml");

            return new ModuleStatusManager(storeFilePath);
        }

        private IModuleDataVersionManager CreateModuleDataVersionManager()
        {
            string storeFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, "module-data-version.xml");

            return new ModuleDataVersionManager(storeFilePath);
        }

        private IApplicationPackageManager CreateApplicationPackageManager()
        {
            ApplicationPackageManagerConfiguration configuration = new ApplicationPackageManagerConfiguration
            {
                LocalPackageSource = Path.Combine(HttpRuntime.AppDomainAppPath, "packages"),
                SourcePackageSource = "http://localhost/nugetserver",
                Project = new WebApplicationFileSystem(HttpRuntime.AppDomainAppPath),
                ConfigurationFileName = "web.config"
            };

            return new ApplicationPackageManager(configuration);
        }

    }

}
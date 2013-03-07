using System.Web.Routing;
using CMS.Core;
using Ninject;

namespace CMS.WebApplication
{

    public class Module : ModuleBase
    {

        public override string Name
        {
            get
            {
                return "CMS.WebApplication";
            }
        }

        public override void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMenuManager>().To<MenuManager>().InSingletonScope();
            kernel.Bind<ILocalPackageModelProvider>().To<LocalPackageModelProvider>().InSingletonScope();
            kernel.Bind<ISourcePackageModelProvider>().To<SourcePackageModelProvider>().InSingletonScope();
        }

        public override void Load()
        {
            RegisterRoutes(RouteTable.Routes);
            MenuItem[] items = new MenuItem[] {
                new MenuItem {
                    Title = "Installed packages",
                    Url = () => RouteTable.Routes.GetRouteUrl("cms.webapplication.localpackage.list", null),
                    RouteNamePrefix = "cms.webapplication.localpackage"
                },
                new MenuItem {
                    Title = "Available packages",
                    Url = () => RouteTable.Routes.GetRouteUrl("cms.webapplication.sourcepackage.list", null),
                    RouteNamePrefix = "cms.webapplication.sourcepackage"
                }
            };
            MenuItemGroup group = new MenuItemGroup
            {
                Title = "Packages"
            };
            group.MenuItems.AddRange(items);
            IMenuManager manager = Locator.Get<IMenuManager>();
            manager.MenuItemGroups.Add(group);
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRouteWithName("cms.webapplication.home", "cms.webapplication/home.aspx", "~/Modules/CMS.WebApplication/Pages/Default.aspx");
            routes.MapPageRouteWithName("cms.webapplication.sourcepackage.list", "cms.webapplication/packages/source.aspx", "~/Modules/CMS.WebApplication/Pages/SourcePackages.aspx");
            routes.MapPageRouteWithName("cms.webapplication.sourcepackage.details", "cms.webapplication/packages/source/{id}.aspx", "~/Modules/CMS.WebApplication/Pages/SourcePackage.aspx");
            routes.MapPageRouteWithName("cms.webapplication.sourcepackage.install", "cms.webapplication/packages/source/install/{id}.aspx", "~/Modules/CMS.WebApplication/Pages/InstallSourcePackage.aspx");
            routes.MapPageRouteWithName("cms.webapplication.localpackage.list", "cms.webapplication/packages/local.aspx", "~/Modules/CMS.WebApplication/Pages/LocalPackages.aspx");
            routes.MapPageRouteWithName("cms.webapplication.localpackage.details", "cms.webapplication/packages/local/{id}.aspx", "~/Modules/CMS.WebApplication/Pages/LocalPackage.aspx");
            routes.MapPageRouteWithName("cms.webapplication.localpackage.updates", "cms.webapplication/packages/local/updates/{id}.aspx", "~/Modules/CMS.WebApplication/Pages/LocalPackageUpdates.aspx");
            routes.MapPageRouteWithName("cms.webapplication.localpackage.update", "cms.webapplication/packages/local/update/{id}/{version}.aspx", "~/Modules/CMS.WebApplication/Pages/UpdateLocalPackage.aspx");
            routes.MapPageRouteWithName("cms.webapplication.localpackage.remove", "cms.webapplication/packages/local/remove/{id}.aspx", "~/Modules/CMS.WebApplication/Pages/RemoveLocalPackage.aspx");
        }

    }

}
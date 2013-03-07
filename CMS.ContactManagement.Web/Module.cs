using System.Web.Routing;

using CMS.Core;
using CMS.WebApplication;

namespace CMS.ContactManagement.Web
{

    public class Module : ModuleBase
    {

        public override string Name
        {
            get
            {
                return "CMS.ContactManagement.Web";
            }
        }

        public override void Load()
        {
            base.Load();
            RegisterRoutes(RouteTable.Routes);
            MenuItem[] items = new MenuItem[] {
                new MenuItem {
                    Title = "Contacts",
                    Url = () => RouteTable.Routes.GetRouteUrl("cms.contactmanagement.contact.list", null),
                    RouteNamePrefix = "cms.contactmanagement.contact"
                }
            };
            MenuItemGroup group = new MenuItemGroup
            {
                Title = "Contact management"
            };
            group.MenuItems.AddRange(items);
            IMenuManager manager = Locator.Get<IMenuManager>();
            manager.MenuItemGroups.Add(group);
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRouteWithName("cms.contactmanagement.contact.list", "cms.contactmanagement/contacts.aspx", "~/Modules/CMS.ContactManagement/Pages/Contacts.aspx");
        }

    }

}
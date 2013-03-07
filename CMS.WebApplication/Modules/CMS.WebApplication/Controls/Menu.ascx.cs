using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

using CMS.Core;

namespace CMS.WebApplication.Controls
{

    public partial class Menu : System.Web.UI.UserControl
    {

        protected IEnumerable<MenuItemGroup> MenuItemGroups
        {
            get
            {
                IMenuManager manager = Locator.Get<IMenuManager>();
                if (manager != null)
                {
                    return manager.MenuItemGroups.AsEnumerable();
                }
                return Enumerable.Empty<MenuItemGroup>();
            }
        }

        protected string CurrentRouteName
        {
            get
            {
                if (Page.RouteData != null && Page.RouteData.DataTokens != null)
                {
                    object value = Page.RouteData.DataTokens["RouteName"];
                    if (value != null)
                    {
                        return value.ToString();
                    }
                }
                return null;
            }
        }

    }

}
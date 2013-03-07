using System.Collections.Generic;

namespace CMS.WebApplication
{

    public interface IMenuManager
    {

        List<MenuItemGroup> MenuItemGroups { get; }

    }

}
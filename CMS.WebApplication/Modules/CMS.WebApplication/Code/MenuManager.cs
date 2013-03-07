using System.Collections.Generic;

namespace CMS.WebApplication
{

    public sealed class MenuManager : IMenuManager
    {

        private readonly List<MenuItemGroup> mGroups = new List<MenuItemGroup>();

        public List<MenuItemGroup> MenuItemGroups
        {
            get
            {
                return mGroups;
            }
        }

    }

}
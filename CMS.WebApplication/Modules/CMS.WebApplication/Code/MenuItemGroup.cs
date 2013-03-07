using System.Collections.Generic;

namespace CMS.WebApplication
{

    public sealed class MenuItemGroup
    {

        private readonly List<MenuItem> mItems = new List<MenuItem>();

        public string Title { get; set; }

        public List<MenuItem> MenuItems
        {
            get
            {
                return mItems;
            }
        }

    }

}
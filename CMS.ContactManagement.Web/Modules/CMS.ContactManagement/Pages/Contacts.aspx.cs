using System;
using System.Collections.Generic;
using System.Linq;

using CMS.ContactManagement.Contract;
using CMS.Core;

namespace CMS.ContactManagement.Web
{

    public partial class Contacts : System.Web.UI.Page
    {

        public IEnumerable<Contact> Model { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Model = CreateModel();
            DataBind();
        }

        private IEnumerable<Contact> CreateModel()
        {
            IContactManager manager = Locator.Get<IContactManager>();
            if (manager == null)
            {
                return Enumerable.Empty<Contact>();
            }

            return manager.GetContacts();
        }

    }

}
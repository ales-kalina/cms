using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.ContactManagement.Contract;

namespace CMS.ContactManagement.Web
{

    public partial class ContactList : System.Web.UI.UserControl
    {

        public IEnumerable<Contact> Model { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBind();
        }

    }

}
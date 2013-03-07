using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.ContactManagement.Contract;

namespace CMS.ContactManagement.Web.Controls
{

    public partial class ContactListItem : System.Web.UI.UserControl
    {

        public Contact Model { get; set; }

    }

}
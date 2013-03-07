using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core;

namespace CMS.WebApplication.Controls
{

    public partial class PackageManagerMessageList : System.Web.UI.UserControl
    {

        public IEnumerable<PackageManagerMessage> Model { get; set; }

    }

}
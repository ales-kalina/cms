using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.WebApplication.Models;

namespace CMS.WebApplication.Controls
{

    public partial class LocalPackageUpdateList : System.Web.UI.UserControl
    {

        public IEnumerable<LocalPackageUpdateModel> Source { get; set; }

        public override void DataBind()
        {
            RepeaterLocalPackageUpdates.DataSource = Source;
            base.DataBind();
        }

    }

}
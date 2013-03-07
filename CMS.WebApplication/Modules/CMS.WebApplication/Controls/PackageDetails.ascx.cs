using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NuGet;
using System.Runtime.Versioning;

namespace CMS.WebApplication.Controls
{

    public partial class PackageDetails : UserControl
    {

        public IPackage Package { get; set; }
        public FrameworkName TargetFramework { get; set; }

    }

}
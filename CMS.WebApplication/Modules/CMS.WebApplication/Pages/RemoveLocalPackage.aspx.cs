using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NuGet;
using System.Reflection;
using CMS.Core;
using System.IO;

namespace CMS.WebApplication.Pages
{

    public partial class RemoveLocalPackage : System.Web.UI.Page
    {

        protected IEnumerable<PackageManagerMessage> Messages { get; private set; }
        protected IPackage Package { get; private set; }
        protected IEnumerable<IModule> Modules { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string packageId = Page.RouteData.Values["id"].ToString();
            RemovePackage(packageId);
            DataBind();
        }

        private void RemovePackage(string packageId)
        {
            IApplicationPackageManager packageManager = Locator.Get<IApplicationPackageManager>();
            IApplicationManager manager = Locator.Get<IApplicationManager>();

            Package = packageManager.SourceRepository.FindPackage(packageId);
            Messages = manager.UninstallPackage(Package);
        }

    }

}
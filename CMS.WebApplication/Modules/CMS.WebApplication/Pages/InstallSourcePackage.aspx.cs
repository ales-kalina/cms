using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NuGet;
using CMS.Core;

namespace CMS.WebApplication.Pages
{

    public partial class InstallSourcePackage : System.Web.UI.Page
    {

        protected IEnumerable<PackageManagerMessage> Messages { get; private set; }
        protected IPackage Package { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string packageId = Page.RouteData.Values["id"].ToString();
            InstallPackage(packageId);
            DataBind();
        }

        private void InstallPackage(string packageId)
        {
            IApplicationPackageManager packageManager = Locator.Get<IApplicationPackageManager>();
            IApplicationManager manager = Locator.Get<IApplicationManager>();

            Package = packageManager.SourceRepository.FindPackage(packageId);
            Messages = manager.InstallPackage(Package);
        }

    }

}
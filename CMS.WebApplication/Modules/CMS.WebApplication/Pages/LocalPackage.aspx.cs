using System;
using System.Web.UI;
using CMS.Core;
using CMS.WebApplication.Models;
using NuGet;

namespace CMS.WebApplication.Pages
{

    public partial class LocalPackage : Page
    {

        protected LocalPackageModel Model { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string packageId = Page.RouteData.Values["id"].ToString();
            Model = CreateModel(packageId);
            DataBind();
        }

        private LocalPackageModel CreateModel(string packageId)
        {
            IApplicationPackageManager packageManager = Locator.Get<IApplicationPackageManager>();
            IPackage package = packageManager.LocalRepository.FindPackage(packageId);
            ILocalPackageModelProvider modelProvider = Locator.Get<ILocalPackageModelProvider>();
            
            return modelProvider.CreateModel(package);
        }

    }

}
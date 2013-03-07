using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NuGet;
using CMS.WebApplication.Models;
using CMS.Core;

namespace CMS.WebApplication.Pages
{

    public partial class SourcePackage : System.Web.UI.Page
    {

        private SourcePackageModel mModel;

        protected string PackageId
        {
            get
            {
                return Page.RouteData.Values["id"].ToString();
            }
        }

        protected SourcePackageModel Model
        {
            get
            {
                if (mModel == null)
                {
                    IPackage package = Locator.Get<IApplicationPackageManager>().SourceRepository.FindPackage(PackageId);
                    mModel = new SourcePackageModel
                    {
                        Package = package,
                        IsInstalled = Locator.Get<IApplicationPackageManager>().LocalRepository.Exists(package.Id)
                    };
                }
                return mModel;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataBind();
        }

    }

}
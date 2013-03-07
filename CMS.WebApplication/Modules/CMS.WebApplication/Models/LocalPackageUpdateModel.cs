using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NuGet;

namespace CMS.WebApplication.Models
{

    public sealed class LocalPackageUpdateModel
    {

        public IPackage Package { get; set; }
        public IPackage UpdatePackage { get; set; }

    }

}
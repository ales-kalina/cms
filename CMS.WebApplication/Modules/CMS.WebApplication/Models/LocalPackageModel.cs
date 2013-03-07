using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NuGet;

namespace CMS.WebApplication.Models
{

    public sealed class LocalPackageModel
    {

        public IPackage Package { get; set; }
        public IPackage UpdatePackage { get; set; }
        public bool HasDependentPackages { get; set; }
        public bool IsProjectPackage { get; set; }

    }

}
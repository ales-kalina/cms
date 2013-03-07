using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NuGet;

namespace CMS.WebApplication.Models
{

    public sealed class SourcePackageModel
    {

        public IPackage Package { get; set; }
        public bool IsInstalled { get; set; }

    }

}
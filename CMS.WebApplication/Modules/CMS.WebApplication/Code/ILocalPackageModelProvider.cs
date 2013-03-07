using System.Collections.Generic;
using CMS.WebApplication.Models;
using NuGet;

namespace CMS.WebApplication
{

    public interface ILocalPackageModelProvider
    {
        LocalPackageModel CreateModel(IPackage package);
        IEnumerable<LocalPackageModel> CreateModels(IEnumerable<IPackage> packages);
    }

}

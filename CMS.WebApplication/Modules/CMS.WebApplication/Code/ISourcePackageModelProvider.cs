using System.Collections.Generic;
using CMS.WebApplication.Models;
using NuGet;

namespace CMS.WebApplication
{

    public interface ISourcePackageModelProvider
    {

        SourcePackageModel CreateModel(IPackage package);
        IEnumerable<SourcePackageModel> CreateModels(IEnumerable<IPackage> packages);

    }

}

using CMS.ContactManagement.Contract;
using CMS.Core;

namespace CMS.ContactManagement
{

    public class Module : ModuleBase
    {

        public override string Name
        {
            get
            {
                return "CMS.ContactManagement";
            }
        }

        public override void RegisterServices(Ninject.IKernel kernel)
        {
            kernel.Bind<IContactManager>().To<ContactManager>().InSingletonScope();
        }

    }

}

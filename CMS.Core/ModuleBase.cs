using Ninject;
using NLog;

namespace CMS.Core
{

    public abstract class ModuleBase : IModule
    {

        private Logger mLog = LogManager.GetLogger("Module");

        public virtual string Name
        {
            get
            {
                return GetType().FullName;
            }
        }

        public virtual void RegisterServices(IKernel kernel)
        {
            mLog.Debug("{0}.RegisterServices", Name);
        }

        public virtual void Load()
        {
            mLog.Debug("{0}.Load", Name);
        }

        public virtual void Unload()
        {
            mLog.Debug("{0}.Unload", Name);
        }

        public virtual void Install()
        {
            mLog.Debug("{0}.Install", Name);
        }

        public virtual void Update()
        {
            mLog.Debug("{0}.Update", Name);
        }

        public virtual void Remove()
        {
            mLog.Debug("{0}.Remove", Name);
        }

    }

}
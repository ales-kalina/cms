using Ninject;

namespace CMS.Core
{

    public interface IModule
    {

        string Name { get; }
        void RegisterServices(IKernel kernel);
        void Load();
        void Unload();
        void Install();
        void Update();
        void Remove();

    }

}
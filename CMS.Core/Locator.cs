using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace CMS.Core
{

    public static class Locator
    {

        internal static IKernel Kernel { get; set; }

        public static T Get<T>()
        {
            try
            {
                return Kernel.Get<T>();
            }
            catch (ActivationException)
            {
                return default(T);
            }
        }

        public static IEnumerable<T> GetAll<T>()
        {
            try
            {
                return Kernel.GetAll<T>();
            }
            catch (ActivationException)
            {
                return Enumerable.Empty<T>();
            }
        }

    }

}
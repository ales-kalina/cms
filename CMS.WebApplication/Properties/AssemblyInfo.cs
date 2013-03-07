using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;

using CMS.WebApplication;

[assembly: PreApplicationStartMethod(typeof(WebApplicationBootstrapper), "Start")]

[assembly: AssemblyTitle("CMS.WebApplication")]
[assembly: AssemblyDescription("Provides a foundation for building web applications based on Acme CMS.")]
[assembly: AssemblyCompany("Acme software")]
[assembly: AssemblyProduct("CMS.WebApplication")]
[assembly: AssemblyCopyright("© 2012 Acme software")]

[assembly: ComVisible(false)]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.2.4")]

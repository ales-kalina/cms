using System.Web;
using System.Web.Routing;

namespace CMS.WebApplication
{

    public static class RouteCollectionExtensions
    {

        public static Route MapPageRouteWithName(this RouteCollection routes, string routeName, string routeUrl, string filePath)
        {
            Route route = routes.MapPageRoute(routeName, routeUrl, filePath);
            route.DataTokens = new RouteValueDictionary();
            route.DataTokens["RouteName"] = routeName;
            return route;
        }

        public static string GetRouteUrl(this RouteCollection routes, string routeName, object routeParameters)
        {
            RouteValueDictionary parameters = new RouteValueDictionary(routeParameters);
            VirtualPathData pathData = RouteTable.Routes.GetVirtualPath(HttpContext.Current.Request.RequestContext, routeName, parameters);
            if (pathData != null)
            {
                return pathData.VirtualPath;
            }
            return null;
        }

    }

}
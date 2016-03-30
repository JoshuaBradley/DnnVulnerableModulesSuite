using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB.Security.SecurityVulnerabilities.API
{
    using DotNetNuke.Web.Api;
    public class ShoppingCartRouter : IServiceRouteMapper
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="mapRouteManager">The map route manager.</param>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "MVC/SecurityVulnerabilities",
                routeName: "default",
                url: "{controller}/{action}",
                namespaces: new[] { "JB.Security.SecurityVulnerabilities.API", });
        }
    }
}
using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
//using Microsoft.AspNet.FriendlyUrls;

namespace RealERPWEB
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            var settings = new FriendlyUrlSettings();
            // For Ajax Call off RedirectMode.Permanent
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            // var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            // routes.EnableFriendlyUrls(settings);
        }
    }
}

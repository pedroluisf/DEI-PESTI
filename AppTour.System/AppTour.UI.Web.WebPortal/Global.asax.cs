using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppTour.UI.Web.WebPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Paginacao", // Route name
                "{controller}/{action}/{page}", // URL with parameters
                new { controller = "Country", action = "Index", page = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                 "DefaultWithPage", // Route name
                 "{controller}/{action}/{id}/{page}", // URL with parameters
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional } // Parameter defaults
             );
            routes.MapRoute(
                "AppTour", // Route name
                "{controller}/{action}/{model}", // URL with parameters
                new { controller = "Home", action = "Index", model = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Culture", // Route name
                "{controller}/{action}/{lang}/{returnUrl}", // URL with parameters
                new { controller = "Home", action = "Index", lang = UrlParameter.Optional, returnUrl = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "UpdateUserProfile", // Route name
                "{controller}/{action}/{email}/{name}/{oldpassword}/{password}/{repeatpassword}/{username}", // URL with parameters
                new
                {
                    controller = "Account",
                    action = "UpdateProfile",
                    email = UrlParameter.Optional,
                    name = UrlParameter.Optional,
                    oldpassword = UrlParameter.Optional,
                    password = UrlParameter.Optional,
                    repeatpassword = UrlParameter.Optional,
                    username = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                "InsertAttribute", // Route name
                "{controller}/{action}/{PointId}/{attrkey}/{attrvalue}/{attrtype}/{active}", // URL with parameters
                new
                {
                    controller = "Attribute",
                    action = "InsertAttribute",
                    PointId = UrlParameter.Optional,
                    attrkey = UrlParameter.Optional,
                    attrvalue = UrlParameter.Optional,
                    attrtype = UrlParameter.Optional,
                    active = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                "LoginUser", // Route name
                "{controller}/{action}/{username}/{password}/{remember}", // URL with parameters
                new
                {
                    controller = "Account",
                    action = "Login",
                    username = UrlParameter.Optional,
                    password = UrlParameter.Optional,
                    remember = UrlParameter.Optional
                }
            );
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //It's important to check whether session object is ready
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Checking first if there is no value in session 
                //and set default language 
                //this can happen for first user's request
                if (ci == null)
                {
                    //Sets default culture to english invariant
                    string langName = "pt";
                    //Try to get values from Accept lang HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        //Gets accepted list 
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }

                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGame.Filters
{
    public class Permission : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["ID"] != null)
            {

            }
            else
            {
                //kiem tra cookie 
                filterContext.RouteData.Values["controller"] = "Account";
                filterContext.RouteData.Values["action"] = "Login";

                ViewResult result = new ViewResult();
                result.ViewName = "Login";
                result.ViewBag.LoginPage = true;
                filterContext.Result = result;
            }
        }
    }
}
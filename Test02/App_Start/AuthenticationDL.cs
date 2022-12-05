using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Routing;
using Test02.Models;

namespace Test02.App_Start
{
    public class AuthenticationDL: Authentication
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            DaiLy nvSession = (DaiLy)HttpContext.Current.Session["userDL"];
            if (nvSession == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoginDL", action = "Login" }));
            }
            return;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Test02.Models;

namespace Test02.App_Start
{
    public class Authentication : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            NhanVien nvSession = (NhanVien)HttpContext.Current.Session["user"];
            if (nvSession == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoginUser", action = "Login" }));
            }
            return;
        }
    }
}
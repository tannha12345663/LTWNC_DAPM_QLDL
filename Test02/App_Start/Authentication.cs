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
        public string MaChucVu { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //1.Check session  : Đã đăng nhập vào hệ thống = > cho thực hiện filterContext
            //Ngược lại thì cho trở lại trang đăng nhập 
            NhanVien nvSession = (NhanVien)HttpContext.Current.Session["user"];
            if (nvSession == null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoginUser", action = "Login" }));
            }
            else
            {
                #region 2. Check quyền : Có quyền thì cho thực hiện filterContext
                //Ngược lại thì cho trở lại trang đăng nhập  = > Trang từ chối truy cập
                QuanLyDLproEntities2 db = new QuanLyDLproEntities2();
                var count = db.NhanViens.Count(m => m.MaChucVu == nvSession.MaChucVu && m.MaChucVu == MaChucVu);
                if (count != 0)
                {
                    return;
                }
                else
                {
                    var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(
                        new
                        {
                            Controller = "TuChoiTruyCap",
                            action = "TuChoiTruyCap",
                            returnUrl = returnUrl.ToString()
                        }));
                }
                #endregion

            }
            return;

        }
    }
}
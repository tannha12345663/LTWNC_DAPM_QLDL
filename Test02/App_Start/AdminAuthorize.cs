using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Test02.App_Start
{
    public class AdminAuthorize:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //1.Check session  : Đã đăng nhập vào hệ thống = > cho thực hiện filterContext
            //Ngược lại thì cho trở lại trang đăng nhập 

            //2. Check quyền : Có quyền thì cho thực hiện filterContext
            //Ngược lại thì cho trở lại trang đăng nhập  = > Trang từ chối truy cập

        }
    }
}
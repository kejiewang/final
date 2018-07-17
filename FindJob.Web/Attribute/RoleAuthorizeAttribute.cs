using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.Mvc;

namespace Book.Web.Attribute
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var isAuth = false;
            //自己代码写在这里
            var actionDescriptor = filterContext.ActionDescriptor;
            var controller = actionDescriptor.ControllerDescriptor.ControllerName;
            var action = actionDescriptor.ActionName;

            if ((filterContext.RequestContext.HttpContext.User.Identity as FormsIdentity) == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new System.Web.Routing.RouteValueDictionary(
                       new { controller = "home", action = "login" }
                       )
                   );
                return;
            }

            var ticket = (filterContext.RequestContext.HttpContext.User.Identity as FormsIdentity).Ticket;
        
            // var RoleId = 2;
            var RoleId = ticket.Version;
            //var ticket;

            //比较ticket与controller加action关系
            //如果通过
           // FInd.BLL.Home bllHome = new BLL.Home();
           // List<Book.Model.Menu> lst = bllHome.GetList(RoleId, controller, action);
           // if (lst.Count >= 1)
           // {
           //isAuth = true;
          //  }


            if (!isAuth)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "home", action = "login" }
                        )
                    );
            }
            else
            {
                base.OnAuthorization(filterContext);
            }

        }

    }
}

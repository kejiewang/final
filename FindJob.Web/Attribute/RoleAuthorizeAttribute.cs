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
            try
            {
                var isAuth = false;
                var actionDescriptor = filterContext.ActionDescriptor;
                var controller = actionDescriptor.ControllerDescriptor.ControllerName;
                var action = actionDescriptor.ActionName;
                var ticket = (filterContext.RequestContext.HttpContext.User.Identity as FormsIdentity).Ticket;

                var RoleId = ticket.Version;

                //Book.BLL.T_Base_Home bllHome = new BLL.T_Base_Home();
                //List<Book.Model.T_Base_Menu> lst = bllHome.GetMenuList(RoleId, conroller, action);
                String tmp = controller;
                bool Ok = false;
                if(RoleId == 1 && tmp.Equals("admin")  || tmp.Equals("home"))
                {
                    Ok = true;
                }
                if(RoleId == 2 && tmp.Equals("student"))
                {
                    Ok = true;
                }
                if(RoleId == 3 && (tmp.Equals("Enterprise") || tmp.Equals("Job")))
                {
                    Ok = true;
                }

                if (Ok)
                {
                    isAuth = true;
                }
                if (!isAuth)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(
                            new { controller = "home", action = "login" }
                            )
                        );
                }

            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "home", action = "login" }
                        )
                    );
            }
            base.OnAuthorization(filterContext);
        }
    }
}

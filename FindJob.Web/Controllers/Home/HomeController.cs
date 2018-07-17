using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindJob.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// 注册信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult RegisterSave(FindJob.Model.T_Base_User user)
        {

            FindJob.BLL.T_Base_User bll = new FindJob.BLL.T_Base_User();
            bll.RegisterSave(user);
            return RedirectToAction("login");
        }

        public ActionResult Test()
        {
            
            
            return View();
        }

        public void TestGet(FindJob.Model.T_Base_User item)
        {
            //String temp = (String)item;
            int i = 1;
        }
	}
}
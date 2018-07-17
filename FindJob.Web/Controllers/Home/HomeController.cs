using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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


        public ActionResult Check(String LoginName, String PWD)
        {
            FindJob.Model.T_Base_User item = new FindJob.BLL.T_Base_User().Check(LoginName, PWD);
            if (item.LoginName.Equals("-1") && item.Password.Equals("-1"))
                return Redirect("Login");
            else
            {

                //进行记录票据
                FormsAuthentication.SetAuthCookie(LoginName, true);
                var authTicket = new FormsAuthenticationTicket(
                    item.RoleId,
                    item.LoginName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    true,
                    "");
                HttpCookie authCookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(authTicket)
                    );
                Response.Cookies.Add(authCookie);
                return RedirectToAction("index", "home");

            }
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



            HttpPostedFileBase file = Request.Files["Pic"];
            String mainPath = HttpContext.Server.MapPath("../Uploads");
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
            }
            String mainPath2 = Path.Combine(mainPath, Path.GetFileName("" + user.LoginName));
            if (!Directory.Exists(mainPath2))
            {
                Directory.CreateDirectory(mainPath2);
            }

            //String FilePath = Path.
            string FilePath = Path.Combine(mainPath2, Path.GetFileName(file.FileName));
            file.SaveAs(FilePath);

            // string  tt = Path.Combine("~//Uploads", Path.GetFileName("" + item.Id));
            user.Pic = Path.Combine("..//Uploads//" + user.LoginName + "//" + Path.GetFileName(file.FileName));//FilePath;
            bll.RegisterSave(user);
            return RedirectToAction("login");
        }



        //public ActionResult Edit(int id = 3)
        //{

        //    FindJob.BLL.T_Base_User bll = new BLL.T_Base_User();
        //    FindJob.Model.T_Base_User item = bll.Find(id);
        //    ViewBag.item = item;
        //    return View();

        //}
        public ActionResult EditSave(FindJob.Model.T_Base_User item)
        {

            HttpPostedFileBase file = Request.Files["Pic"];
            String mainPath = HttpContext.Server.MapPath("../Uploads");
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
            }
            String mainPath2 = Path.Combine(mainPath, Path.GetFileName("" + item.Id));
            if (!Directory.Exists(mainPath2))
            {
                Directory.CreateDirectory(mainPath2);
            }

            //String FilePath = Path.
            string FilePath = Path.Combine(mainPath2, Path.GetFileName(file.FileName));
            file.SaveAs(FilePath);

           // string  tt = Path.Combine("~//Uploads", Path.GetFileName("" + item.Id));
            item.Pic = Path.Combine("..//Uploads//"+item.Id+"//"+ Path.GetFileName(file.FileName));//FilePath;
            FindJob.BLL.T_Base_User bll = new BLL.T_Base_User();
            bll.Update(item);
            return RedirectToAction("login");

        }
        
	}
}
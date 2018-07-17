using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FindJob.Web.Controllers.Enterprise
{
    public class EnterpriseController : Controller
    {
        //
        // GET: /Enterprise/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddInfo()
        {
            return View();
        }

        public ActionResult AddInfoSave(FindJob.Model.T_Base_Enterprise enterprice)
        {
            //待修改
            enterprice.UserId = 1;
            HttpPostedFileBase file = Request.Files["Qualification"];
            String mainPath = HttpContext.Server.MapPath("../Uploads");
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
            }
            String mainPath2 = Path.Combine(mainPath, Path.GetFileName(""+ enterprice.UserId));
            if (!Directory.Exists(mainPath2))
            {
                Directory.CreateDirectory(mainPath2);
            }

            //String FilePath = Path.
            string FilePath = Path.Combine(mainPath2, Path.GetFileName(file.FileName));
            file.SaveAs(FilePath);
            enterprice.Qualification = FilePath;
            


            FindJob.BLL.T_Base_Enterpirse bll = new FindJob.BLL.T_Base_Enterpirse();
            bll.AddInfoSave(enterprice);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateInfo(int id = 8)
        {
            //待修改
            
            FindJob.Model.T_Base_Enterprise enterprise= new FindJob.BLL.T_Base_Enterpirse().GetModel(id);
            ViewBag.enterprise = enterprise;
            return View();
        }

        public ActionResult EditInfoSave(FindJob.Model.T_Base_Enterprise enterprise)
        {
            //待修改
          
            FindJob.BLL.T_Base_Enterpirse bll = new FindJob.BLL.T_Base_Enterpirse();
            bll.Update(enterprise);
            return RedirectToAction("Index");
        }

        
    }
}
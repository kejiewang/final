using System.Collections.Generic;
using System.Web.Mvc;

namespace FindJob.Web.Controllers.Admin
{
    public class AdminController : Controller
    {
        FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
        //
        // GET: /admin/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 企业模块管理
        /// </summary>
        /// <returns></returns>
        public ActionResult EPIndex()
        {
            return View();
        }
        public JsonResult EPGetList(int pageSize, int pageIndex, string EPName)
        {

            List<FindJob.Model.T_Base_Enterprise> lst = new List<FindJob.Model.T_Base_Enterprise>();
            lst = bll.GetList(pageIndex, pageSize, EPName);
            int count = bll.EPCount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult EPDelete(string []Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.EPDelete(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        /// <summary>
        /// 学生模块管理
        /// </summary>
        /// <returns></returns>
        public ActionResult StuIndex()
        {
            return View();
        }
        public JsonResult StuGetList(int pageSize, int pageIndex, string StuName,string SchoolName,string MajorName,string ClassName)
        {

            List<FindJob.Model.T_Base_Student> lst = new List<FindJob.Model.T_Base_Student>();
            lst = bll.GetList(pageIndex, pageSize, StuName, SchoolName,MajorName, ClassName);
            int count = bll.EPCount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult StuDelete(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.StuDelete(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        /// <summary>
        /// 求职学生信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyJobIndex()
        {
            return View();
        }
        public JsonResult ApplyJobGetList(int pageSize, int pageIndex, string StuName, string SchoolName, string MajorName, string ClassName)
        {

            List<FindJob.Model.T_Relation_ApplyJob> lst = new List<FindJob.Model.T_Relation_ApplyJob>();
            lst = bll.ApplyJobGetList(pageIndex, pageSize, StuName, SchoolName, MajorName, ClassName);
            int count = bll.ApplyJobCount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult ApplyJobDelete(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.ApplyJobDelete(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        /// <summary>
        /// 企业审核
        /// </summary>
        /// <returns></returns>
        public ActionResult EPCheck()
        {
            return View();
        }
        public JsonResult EPCheckGetList(int pageSize, int pageIndex, string EPName)
        {

            List<FindJob.Model.T_Base_Enterprise> lst = new List<FindJob.Model.T_Base_Enterprise>();
            lst = bll.EPCheckGetList(pageIndex, pageSize, EPName);
            int count = bll.EPCheckCount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult EPCheckPass(string[] Ids)
        {
            
            bll.EPCheckPass(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        public JsonResult EPCheckDelete(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.EPCheckDelete(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        /// <summary>
        /// 学生信息审核
        /// </summary>
        /// <returns></returns>
        public ActionResult StuCheckIndex()
        {
            return View();
        }
        public JsonResult StuCheckGetList(int pageSize, int pageIndex, string StuName, string SchoolName, string MajorName, string ClassName)
        {

            List<FindJob.Model.T_Base_Student> lst = new List<FindJob.Model.T_Base_Student>();
            lst = bll.StuCheckGetList(pageIndex, pageSize, StuName, SchoolName, MajorName, ClassName);
            int count = bll.EPCount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult StuCheckPass(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.StuCheckPass(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        public JsonResult StuCheckCancel(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.StuCheckCancel(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        /// <summary>
        /// 就业学生信息
        /// </summary>
        public ActionResult EIIndex()
        {
            return View();
        }
        public JsonResult EIGetList(int pageSize, int pageIndex, string StuName, string SchoolName, string MajorName, string ClassName)
        {

            List<FindJob.Model.T_Base_EI> lst = new List<FindJob.Model.T_Base_EI>();
            lst = bll.EIGetList(pageIndex, pageSize, StuName, SchoolName, MajorName, ClassName);
            int count = bll.EICount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult EIDelete(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.EIDelete(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        /// <summary>
        /// 就业学生信息学生审核
        /// </summary>
        public JsonResult EICheckGetList(int pageSize, int pageIndex, string StuName, string SchoolName, string MajorName, string ClassName)
        {

            List<FindJob.Model.T_Base_EI> lst = new List<FindJob.Model.T_Base_EI>();
            lst = bll.EICheckGetList(pageIndex, pageSize, StuName, SchoolName, MajorName, ClassName);
            int count = bll.EICheckCount();
            return Json(new { total = count, rows = lst });
            //return Json(lst);
        }
        public JsonResult EICheckPass(string[] Ids)
        {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.EICheckPass(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        public JsonResult EICheckCancel(string[] Ids) {
            FindJob.BLL.T_Base_Admin bll = new BLL.T_Base_Admin();
            bll.EICheckCancel(Ids);
            return Json(new FindJob.Model.Message() { Code = 1, Content = "删除成功" });
        }
        public ActionResult EICheckIndex()
        {
            return View();
        }
        /// <summary>
        /// 就业统计
        /// </summary>
        /// <returns></returns>
        public ActionResult EISaticalIndex()
        {
            int jp=bll.JobPercent();
            Model.JobSalary salary = bll.JobSalary();//平均薪资
            ViewBag.jp = jp;//就业率
            ViewBag.avesalary = salary.JobAveSalary;
            ViewBag.maxsalary = salary.JobMaxSalary;
            ViewBag.minsalary = salary.JobMinSalary;
            int relatemajor=bll.RelateMajor();//专业相关

            int value = 0;
            if (salary.JobAveSalary >= 3000 && salary.JobAveSalary <= 4000)
                value = 10;
            else if (salary.JobAveSalary >= 4000 && salary.JobAveSalary <= 5000)
                value = 15;
            else if (salary.JobAveSalary >= 5000 && salary.JobAveSalary <= 6000)
                value = 20;
            else if (salary.JobAveSalary >= 6000 )
                value = 25;
            else
                value = 5;
            ViewBag.eisatical = (int)(relatemajor * 0.25 + jp * 0.5 + value);
            return View();
        }
       
    }
}
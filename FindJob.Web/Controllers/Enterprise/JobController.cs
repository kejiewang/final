using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindJob.Web.Controllers.Enterprise
{
    public class JobController : Controller
    {
        // GET: Job
        int PageSize = 3;
        int MaxPageIndex = 5;
        public ActionResult Index()
        {
            FindJob.BLL.T_Base_Job bll = new BLL.T_Base_Job();
            //List<FindJob.Model.T_Base_Job> lst = bll.GetList(1, PageSize);

            FindJob.Model.T_Base_Job_Page page = bll.GetListPage(1, PageSize);
            ViewBag.MaxPageIndex = MaxPageIndex;
            ViewBag.count = page.count;
            ViewBag.lst = page.jobList;
            ViewBag.PageSize = PageSize;

            return View();
        }
        public JsonResult GetList(int currentPage, string Id, string JobName)
        {
            FindJob.BLL.T_Base_Job bll = new BLL.T_Base_Job();

            List<FindJob.Model.T_Base_Job> lst = bll.GetList(currentPage, PageSize, Id, JobName);
            return Json(lst);
        }



        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddSave(string JobName,string Memo)
        {
            FindJob.Model.T_Base_Job job = new Model.T_Base_Job();
            job.JobName = JobName;
            job.Memo = Memo;


            FindJob.BLL.T_Base_Job bll = new BLL.T_Base_Job();
            int result = bll.Add(job);

            //返回操作结果，受影响行数
            return Content("<script>alert('成功插入" + result + "行');window.location.href='/Job/Index';</script>");
            //return Redirect("/FindJob/Index");

        }


        public JsonResult DeleteJson(int Id)
        {

            FindJob.BLL.T_Base_Job bll = new BLL.T_Base_Job();
            int result = bll.Delete(Id);
            FindJob.Model.Message message;

            if (result > 0)
                message = new FindJob.Model.Message() { Code = 200, Content = "删除成功" };
            else
                message = new Model.Message() { Code = 500, Content = "删除失败" };

            return Json(message);

        }

        public ActionResult Update(int Id)
        {

            FindJob.Model.T_Base_Job job = (new FindJob.BLL.T_Base_Job()).GetModel(Id);

            ViewBag.job = job;
            return View();
        }

        public ActionResult UpdateSave(FindJob.Model.T_Base_Job job)
        {
            FindJob.BLL.T_Base_Job bll = new BLL.T_Base_Job();
            int result = bll.Update(job);

            return Content("<script>alert('成功修改" + result + "行');window.location.href='/Job/Index';</script>");
        }
        public JsonResult GetSearch(string JobName, int matchCount = 10)
        {
            JobName = JobName.Trim();
            FindJob.BLL.T_Base_Job bll = new BLL.T_Base_Job();
            List<FindJob.Model.T_Base_Job> lst = bll.GetSearch(JobName, matchCount);

            return Json(lst);
        }
    }
}
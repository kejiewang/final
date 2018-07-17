using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindJob.Web.Controllers.Student
{
    public class StudentController : Controller
    {
        public int userId = 1;
        FindJob.BLL.T_Base_Studet bll = new BLL.T_Base_Studet();
        // GET: /Student/
        public ActionResult Index(int userId=1)
        {
            Model.T_Base_Student stu= bll.GetModel(userId);
            ViewBag.stu = stu;
            return View();
        }

        public ActionResult AddInfo()
        {
            return View();
        }

        public ActionResult AddInfoSave(FindJob.Model.T_Base_Student item)
        {
            bll.AddInfoSave(item);
            return RedirectToAction("index");
        }

        public ActionResult UpdateInfo()
        {
            Model.T_Base_Student stu = bll.GetModel(userId);
            ViewBag.stu = stu;
            return View();
        }

        public ActionResult UpdateInfoSave(FindJob.Model.T_Base_Student item)
        {
            item.UserId = userId;
            bll.Update(item);
            return RedirectToAction("index");
        }

        public ActionResult EISubmitIndex()
        {
            return View();
        }
        public ActionResult EISubmitSave(Model.T_Base_EI eI)
        {
            //EI.StudentId对应的是student表中的id<--主键
            eI.StudentId = bll.GetModel(userId).Id;
            bll.EISubmitSave(eI);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 浏览器企业工作功能
        /// </summary>
        /// <returns></returns>
        public ActionResult LookForJobIndex()
        {
            FindJob.BLL.T_Base_Admin jbbll = new BLL.T_Base_Admin();
            FindJob.BLL.T_Base_Enterpirse infobll = new BLL.T_Base_Enterpirse();

            List<FindJob.Model.T_Base_Enterprise> lst=jbbll.GetList(1, 100, "");
            
            foreach (var item in lst)
            {
                int EPId = item.Id;
                item.JobInfoList= bll.GetRecruitInfoList(EPId);         
            }
            ViewBag.AllList = lst;
            return View();
        }

        public ActionResult JobIndex(int EPId,string JobName)
        {
            FindJob.BLL.T_Base_Enterpirse infobll = new BLL.T_Base_Enterpirse();
            Model.T_Relation_RecruitInfo jobinfo=infobll.GetRecruitInfo(EPId, JobName);
            ViewBag.jobinfo= jobinfo;
            return View();
        }

        public ActionResult ApplyJob(int EPId, string JobName)
        {
            int userId = 1;
            bll.ApplyJob(userId, EPId, JobName);
            return RedirectToAction("index");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindJob.Web.Controllers.Student
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddInfo()
        {
            return View();
        }

        //public ActionResult AddInfoSave(FindJob.Model.T_Base_Student item)
        //{
        //    //待修改

        //}

	}
}
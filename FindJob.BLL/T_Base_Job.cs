using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.BLL
{
    public class T_Base_Job
    {
        public List<FindJob.Model.T_Base_Job> GetAll()
        {
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();
            return dal.GetAll();
        }

        public List<FindJob.Model.T_Base_Job> GetList(int currentPage, int pageSize, string id, string JobName)
        {
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();
            List<FindJob.Model.T_Base_Job> lst = new List<Model.T_Base_Job>();
            lst = dal.GetList(currentPage, pageSize, id, JobName);
            return lst;
        }

        public FindJob.Model.T_Base_Job_Page GetListPage(int CurrentPage, int PageSize)
        {
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();
            List<FindJob.Model.T_Base_Job> list = dal.GetList(CurrentPage, PageSize, "", "");
            int count = dal.GetCount();

            FindJob.Model.T_Base_Job_Page page = new Model.T_Base_Job_Page();
            page.jobList = list;
            page.count = count;
            return page;
        }

        public int Add(FindJob.Model.T_Base_Job job)
        {

            int result = 0;
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();
            result = dal.Add(job);
            return result;

        }

        public int Delete(int Id)
        {
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();
            int result = dal.Delete(Id);
            return result;

        }

        public FindJob.Model.T_Base_Job GetModel(int Id)
        {

            FindJob.Model.T_Base_Job job = new Model.T_Base_Job();
            job = (new FindJob.DAL.T_Base_Job()).GetModel(Id);

            return job;
        }

        public int Update(FindJob.Model.T_Base_Job job)
        {
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();

            int result = dal.Update(job);

            return result;

        }

        public List<FindJob.Model.T_Base_Job> GetSearch(string JobName, int matchCount = 10)
        {
            JobName = JobName.Trim();
            FindJob.DAL.T_Base_Job dal = new DAL.T_Base_Job();
            List<FindJob.Model.T_Base_Job> lst = dal.GetSearch(JobName, matchCount);

            return lst;
        }

    }
}

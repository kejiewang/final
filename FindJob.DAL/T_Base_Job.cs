using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FindJob.DAL
{
    public class T_Base_Job
    {
        public string sqlstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;


        public List<FindJob.Model.T_Base_Job> GetAll()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_Job";
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Job> lst = new List<FindJob.Model.T_Base_Job>();

            while (dr.Read())
            {
                FindJob.Model.T_Base_Job job = new FindJob.Model.T_Base_Job();
                job.Id = Convert.ToInt32(dr["Id"]);
                job.JobName = Convert.ToString(dr["JobName"]);
                job.Memo = Convert.ToString(dr["Memo"]);
                lst.Add(job);
            }

            dr.Close();
            co.Close();
            return lst;

        }

        public int Add(FindJob.Model.T_Base_Job job)
        {

            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "insert into T_Base_Job (JobName,Memo) values (@JobName,@Memo)";

            cm.Parameters.AddWithValue("@JobName", job.JobName);
            cm.Parameters.AddWithValue("@Memo", job.Memo);


            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Delete(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "delete from T_Base_Job where Id=" + Id;
            //cm.Parameters.AddWithValue("@FindJobJobName", Id);
            int result = cm.ExecuteNonQuery();
            return result;

        }

        public FindJob.Model.T_Base_Job GetModel(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_Job where id=@Id";
            cm.Parameters.AddWithValue("@Id", Id);
            SqlDataReader dr = cm.ExecuteReader();

            FindJob.Model.T_Base_Job job = null;
            while (dr.Read())
            {
                job = new FindJob.Model.T_Base_Job();
                job.Id = Convert.ToInt32(dr["Id"]);
                job.JobName = Convert.ToString(dr["JobName"]);
                job.Memo = Convert.ToString(dr["Memo"]);

            }
            dr.Close();
            co.Close();
            return job;


        }

        public int Update(FindJob.Model.T_Base_Job job)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "update T_Base_Job set JobName=@JobName,Memo=@Memo where Id=@Id";

            cm.Parameters.AddWithValue("@JobName", job.JobName);
            cm.Parameters.AddWithValue("@Memo", job.Memo);
            cm.Parameters.AddWithValue("@Id", job.Id);

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public List<FindJob.Model.T_Base_Job> GetList(int CurrentPage, int PageSize, string Id, string JobName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            Id = "'%" + Id + "%'";
            JobName = "'%" + JobName + "%'";
            cm.CommandText = "select top " + PageSize + " " +
                "* from  T_Base_Job where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from T_Base_Job where Id like " + Id + " and JobName like " + JobName + ")     and  Id like " + Id + " and JobName like " + JobName;
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Job> lst = new List<FindJob.Model.T_Base_Job>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_Job job = new FindJob.Model.T_Base_Job();
                job.Id = Convert.ToInt32(dr["Id"]);
                job.JobName = Convert.ToString(dr["JobName"]);
                job.Memo = Convert.ToString(dr["Memo"]);
                lst.Add(job);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public int GetCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from T_Base_Job";
            cm.Connection = co;
            object result = cm.ExecuteScalar();
            co.Close();
            return (int)result;
        }

        public List<FindJob.Model.T_Base_Job> GetSearch(string JobName, int matchCount)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select top " + matchCount + " * from T_Base_Job where JobName like '%" + JobName + "%'";
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Job> lst = new List<FindJob.Model.T_Base_Job>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_Job item = new FindJob.Model.T_Base_Job();
                item.Id = Convert.ToInt32(dr["Id"]);
                item.JobName = Convert.ToString(dr["JobName"]);
                item.Memo = Convert.ToString(dr["Memo"]);
                lst.Add(item);
            }
            dr.Close();
            co.Close();
            return lst;
        }

    }
}
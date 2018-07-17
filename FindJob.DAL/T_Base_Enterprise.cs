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
    public class T_Base_Enterprise
    {
        public string connstring = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

        public int AddInfoSave(FindJob.Model.T_Base_Enterprise enterprise)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.Parameters.AddWithValue("@Name", enterprise.Name);
            cm.Parameters.AddWithValue("@Tel", enterprise.Tel);
            cm.Parameters.AddWithValue("@Address", enterprise.Address);
            cm.Parameters.AddWithValue("@Introduction", enterprise.Introduction);
            cm.Parameters.AddWithValue("@Qualification", enterprise.Qualification);
            cm.Parameters.AddWithValue("@IsChecked", false);
            cm.Parameters.AddWithValue("@UserId", enterprise.Id);
            cm.CommandText = "insert into T_Base_EnterpriseCheck values(@Name,@Tel,@Address,@Introduction,@Qualification,@IsChecked,@UserId)";
            int result = cm.ExecuteNonQuery();


            co.Close();
            return result;
        }

        public FindJob.Model.T_Base_Enterprise GetModel(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_Enterprise where id=@Id";
            cm.Parameters.AddWithValue("@Id", Id);
            SqlDataReader dr = cm.ExecuteReader();

            FindJob.Model.T_Base_Enterprise enterprise = null;
            while (dr.Read())
            {
                enterprise = new Model.T_Base_Enterprise();
                enterprise.Id = Convert.ToInt32(dr["Id"]);
                enterprise.Name = Convert.ToString(dr["Name"]);
                enterprise.Tel = Convert.ToString(dr["Tel"]);
                enterprise.Address = Convert.ToString(dr["Address"]);
                enterprise.Introduction = Convert.ToString(dr["Introduction"]);
                enterprise.Qualification = Convert.ToString(dr["Qualification"]);
                enterprise.IsChecked = Convert.ToBoolean(dr["IsChecked"]);
                enterprise.UserId = Convert.ToInt32(dr["UserId"]);
            }
            dr.Close();
            co.Close();
            return enterprise;
        }

        public int Update(FindJob.Model.T_Base_Enterprise enterprise)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "update T_Base_EnterpriseCheck set Name=@Name, Tel=@Tel, Address=@Address," +
                "Introduction=@Introduction,Qualification=@Qualification,IsChecked=@IsChecked,UserId=@UserId where Id=@Id";
            cm.Parameters.AddWithValue("@Id", enterprise.Id);
            cm.Parameters.AddWithValue("@Name", enterprise.Name);
            cm.Parameters.AddWithValue("@Tel", enterprise.Tel);
            cm.Parameters.AddWithValue("@Address", enterprise.Address);
            cm.Parameters.AddWithValue("@Introduction", enterprise.Introduction);
            cm.Parameters.AddWithValue("@Qualification", enterprise.Qualification);
            cm.Parameters.AddWithValue("@IsChecked", enterprise.IsChecked);       
            cm.Parameters.AddWithValue("@UserId", enterprise.UserId);
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }


        //返回该企业对应所有的已发布招聘岗位
        public List<Model.T_Relation_RecruitInfo> GetRecruitInfoList(int EPId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from V_Recruit where EnterpriseId=" + EPId;
            SqlDataReader dr = cm.ExecuteReader();
            List<Model.T_Relation_RecruitInfo> lst = new List<Model.T_Relation_RecruitInfo>();
            while (dr.Read())
            {
                Model.T_Relation_RecruitInfo item = new Model.T_Relation_RecruitInfo();
                item.JobName = Convert.ToString(dr["JobName"]);
                lst.Add(item);
            } 
            dr.Close();
            co.Close();
            return lst;
        }
        public Model.T_Relation_RecruitInfo GetRecruitInfo(int EPId,string JobName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            Model.T_Relation_RecruitInfo info = new Model.T_Relation_RecruitInfo();
            
            cm.CommandText = "select id from T_Base_Job where jobname ='" + JobName + "'";
            //SqlDataReader dr2 = cm.ExecuteReader();
            //dr2.Close();
            int jobId=(int)cm.ExecuteScalar();
            
            cm.CommandText = "select T_Relation_RecruitInfo.*,T_Base_Job.JobName from T_Relation_RecruitInfo,T_Base_Job where T_Base_Job.Id=T_Relation_RecruitInfo.jobid and EnterpriseId=" + EPId+" and jobid="+jobId;
            cm.Connection = co;
            SqlDataReader dr = cm.ExecuteReader();
            while(dr.Read())
            {
                info.JobName = Convert.ToString(dr["JobName"]);
                info.JobId = jobId;
                info.Requirement = Convert.ToString(dr["Requirement"]);
                info.Salary = Convert.ToString(dr["Salary"]);
                info.WorkPlace = Convert.ToString(dr["WorkPlace"]);
                info.Amount = Convert.ToInt32(dr["Amount"]);
                info.EnterpriseId = Convert.ToInt32(dr["EnterpriseId"]);
            }
            dr.Close();
            co.Close();
            return info;
        }
}
}
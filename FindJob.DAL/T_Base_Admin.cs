﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FindJob.Model;

namespace FindJob.DAL
{
    public class T_Base_Admin
    {
        public string constr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
        public List<FindJob.Model.T_Base_Enterprise> GetList(int CurrentPage, int PageSize, string EPName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            //cm.CommandText = "select * from t_base_FindJob";
            EPName = "'%" + EPName + "%'";

            cm.CommandText = "select top " + PageSize + " * from  T_Base_Enterprise where id not in (select top " + PageSize * (CurrentPage - 1) + " id from T_Base_Enterprise where ischecked=1 and Name like " + EPName + ")   and ischecked = 1  and  Name like " + EPName;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Enterprise> lst = new List<Model.T_Base_Enterprise>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_Enterprise EP = new Model.T_Base_Enterprise();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                EP.Id = Convert.ToInt32(dr["Id"]);
                
                EP.Name = Convert.ToString(dr["Name"]);
                EP.Tel = Convert.ToString(dr["Tel"]);
                EP.Address = Convert.ToString(dr["Address"]);
                EP.Introduction = Convert.ToString(dr["Introduction"]);
                EP.Qualification = Convert.ToString(dr["Qualification"]);
                EP.IsChecked = Convert.ToBoolean(dr["IsChecked"]);
                EP.UserId = Convert.ToInt32(dr["UserId"]);
               
                lst.Add(EP);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int EPCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from T_Base_Enterprise where ischecked=1";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        public int EPDelete(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            //cm.CommandText = "delete from t_stock_inItems where headid in (" + ids + ");delete from t_stock_inhead where id in (" + ids + ") ; ";
            cm.CommandText = "delete from T_Base_Enterprise where id in (" + ids + ") ; ";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 学生模块
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="EPName"></param>
        /// <returns></returns>
        public List<FindJob.Model.T_Base_Student> GetList(int CurrentPage, int PageSize, string StuName,string SchoolName, string MajorName, string ClassName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
         
            StuName = "'%" + StuName + "%'";
            SchoolName = "'%" + SchoolName + "%'";
            MajorName = "'%" + MajorName + "%'";
            ClassName = "'%" + ClassName + "%'";

            cm.CommandText = "select top " + PageSize + " * from  T_Base_Student where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from T_Base_Student where ischeck=1 and Name like " + StuName + " and Major like " +MajorName+" and Class like " +ClassName+ " and School like "+SchoolName+") and  Name like " + StuName+ " and Major like " + MajorName + " and Class like " + ClassName+ " and ischeck = 1 and School like " + SchoolName ;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Student> lst = new List<Model.T_Base_Student>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_Student Stu = new Model.T_Base_Student();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                Stu.Id = Convert.ToInt32(dr["Id"]);

                Stu.Name = Convert.ToString(dr["Name"]);
                Stu.Major = Convert.ToString(dr["Major"]);
                Stu.Phone = Convert.ToString(dr["Phone"]);
                Stu.Class = Convert.ToString(dr["Class"]);
                Stu.School = Convert.ToString(dr["School"]);
                Stu.IdCard = Convert.ToString(dr["IdCard"]);
                Stu.UserId = Convert.ToInt32(dr["UserId"]);
                Stu.Gender = Convert.ToString(dr["Gender"]);
                lst.Add(Stu);
            }
            dr.Close();
            co.Close();
            return lst;
        }


        /// <summary>
        /// 学生批量导入
        /// </summary>
        /// <param name="lst">lst为批量导入的学生</param>
        public void ImportStudent(List<Model.T_Base_Student> lst)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlTransaction tran = co.BeginTransaction();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.Transaction = tran;

            try
            {
                cm.Parameters.Clear();
                foreach(FindJob.Model.T_Base_Student item in lst)
                {
                    cm.Parameters.Clear();
                    cm.CommandText = "insert into t_base_user (LoginName, Password, RoleId, Phone) values (@LoginName, @Password, @RoleId, @Phone)";
                    cm.Parameters.AddWithValue("@LoginName", item.Phone);
                    cm.Parameters.AddWithValue("@Password", "123456");
                    cm.Parameters.AddWithValue("@RoleId", "2");
                    cm.Parameters.AddWithValue("@Phone", item.Phone);
                    cm.ExecuteNonQuery();
                    //cm.Parameters.Clear();
                    cm.CommandText = "select * from t_base_user where LoginName = @LoginName";
                    SqlDataReader dr = cm.ExecuteReader();
                    int UserId;
                    while (dr.Read())
                    {
                        item.UserId =  UserId = Convert.ToInt32(dr["Id"]);
                    }
                    dr.Close();
                    cm.Parameters.Clear();
                    cm.CommandText = "insert into T_Base_Student (Major, Phone, Class, Gender, Name, School, IdCard, UserId, IsCheck) values(@Major, @Phone, @Class, @Gender, @Name, @School, @IdCard,  @UserId, @IsCheck)";
                    cm.Parameters.AddWithValue("@Major", item.Major);
                    cm.Parameters.AddWithValue("@Phone", item.Phone);
                    cm.Parameters.AddWithValue("@Class", item.Class);
                    cm.Parameters.AddWithValue("@Gender", item.Gender);
                    cm.Parameters.AddWithValue("@Name", item.Name);
                    cm.Parameters.AddWithValue("@IsCheck", true);
                    cm.Parameters.AddWithValue("@School", item.School);
                    cm.Parameters.AddWithValue("@IdCard", item.IdCard);
                    cm.Parameters.AddWithValue("@UserId", item.UserId);
                    cm.ExecuteNonQuery();
                }
                tran.Commit();
                return;
            }catch(Exception ex)
            {
                tran.Rollback(); //错误，回滚

            }
            finally
            {
                co.Close();
            }
        }

        public int StuCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from T_Base_Student where ischeck=1";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        public int StuDelete(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            //cm.CommandText = "delete from t_stock_inItems where headid in (" + ids + ");delete from t_stock_inhead where id in (" + ids + ") ; ";
            cm.CommandText = "delete from T_Base_Student where id in (" + ids + ") ; ";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 求职学生信息管理
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="StuName"></param>
        /// <param name="SchoolName"></param>
        /// <param name="MajorName"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public List<FindJob.Model.T_Relation_ApplyJob> ApplyJobGetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            StuName = "'%" + StuName + "%'";
            SchoolName = "'%" + SchoolName + "%'";
            MajorName = "'%" + MajorName + "%'";
            ClassName = "'%" + ClassName + "%'";

            cm.CommandText = "select top " + PageSize + " * from  [V_ApplyStu] " +
                "where  StuName not in (select top " + PageSize * (CurrentPage - 1) + " StuName from [V_ApplyStu] where StuName like "
                + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName + ") and  StuName like " 
                + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Relation_ApplyJob> lst = new List<Model.T_Relation_ApplyJob>();
            while (dr.Read())
            {
                Model.T_Relation_ApplyJob item = new Model.T_Relation_ApplyJob();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                

                item.StuName = Convert.ToString(dr["StuName"]);
                item.Major = Convert.ToString(dr["Major"]);
                item.School = Convert.ToString(dr["School"]);
                item.Class = Convert.ToString(dr["Class"]);
                item.EnterpriseId = Convert.ToInt32(dr["EnterpriseId"]);
                item.JobName = Convert.ToString(dr["JobName"]);
                item.EPName = Convert.ToString(dr["EPName"]);
                item.WorkPlace = Convert.ToString(dr["WorkPlace"]);
                item.Salary = Convert.ToString(dr["Salary"]);
                item.EPTel = Convert.ToString(dr["Tel"]);
                lst.Add(item);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int ApplyJobCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from [V_ApplyStu]";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        /// <summary>
        /// 企业审核
        /// </summary>
        public List<FindJob.Model.T_Base_Enterprise> EPCheckGetList(int CurrentPage, int PageSize, string EPName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            //cm.CommandText = "select * from t_base_FindJob";
            EPName = "'%" + EPName + "%'";

            cm.CommandText = "select top " + PageSize + " * from  T_Base_EnterpriseCheck where id not in (select top " + PageSize * (CurrentPage - 1) + " id from T_Base_Enterprise where ischecked=0 and Name like " + EPName + ")   and ischecked = 0  and  Name like " + EPName;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Enterprise> lst = new List<Model.T_Base_Enterprise>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_Enterprise EP = new Model.T_Base_Enterprise();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                EP.Id = Convert.ToInt32(dr["Id"]);

                EP.Name = Convert.ToString(dr["Name"]);
                EP.Tel = Convert.ToString(dr["Tel"]);
                EP.Address = Convert.ToString(dr["Address"]);
                EP.Introduction = Convert.ToString(dr["Introduction"]);
                EP.Qualification = Convert.ToString(dr["Qualification"]);
                EP.IsChecked = Convert.ToBoolean(dr["IsChecked"]);
                EP.UserId = Convert.ToInt32(dr["UserId"]);

                lst.Add(EP);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int EPCheckCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from T_Base_EnterpriseCheck where ischecked=0";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        public int EPCheckPass(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from  T_Base_EnterpriseCheck where id ="+ids;
            SqlDataReader dr = cm.ExecuteReader();
            FindJob.Model.T_Base_Enterprise EP = new Model.T_Base_Enterprise();
            while (dr.Read())
            {
                
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                EP.Id = Convert.ToInt32(dr["Id"]);

                EP.Name = Convert.ToString(dr["Name"]);
                EP.Tel = Convert.ToString(dr["Tel"]);
                EP.Address = Convert.ToString(dr["Address"]);
                EP.Introduction = Convert.ToString(dr["Introduction"]);
                EP.Qualification = Convert.ToString(dr["Qualification"]);
                EP.IsChecked = Convert.ToBoolean(dr["IsChecked"]);
                EP.UserId = Convert.ToInt32(dr["UserId"]);
                EP.Tel = EP.Tel.Trim();
            }
            dr.Close();
            cm.CommandText = "select * from  T_Base_Enterprise where Name='" + EP.Name+"'";
            SqlDataReader dr2 = cm.ExecuteReader();
            
            if(dr2!=null)
            {
                cm.CommandText = "update T_Base_Enterprise set tel= '" + EP.Tel + "',address='"+EP.Address+"',introduction='"+EP.Introduction+"' where Name ='" + EP.Name+"'";
            }
            else
            {
                cm.CommandText = "insert into T_Base_Enterprise(tel,name,address,introduction,qualification,ischecked,userId) values('"
                    + EP.Tel + "','"  + EP.Name + "','" + EP.Address + "','" + EP.Introduction + "','" + EP.Qualification + "',1,'" + EP.UserId + "')";
            }
            dr2.Close();
            cm.ExecuteNonQuery();
            //cm.CommandText = "delete from t_stock_inItems where headid in (" + ids + ");delete from t_stock_inhead where id in (" + ids + ") ; ";
            cm.CommandText = "delete from T_Base_EnterpriseCheck where id in (" + ids + ") ; ";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();

            co.Close();
            return result;
        }
        /// <summary>
        /// 学生信息审核
        /// </summary>
        public List<FindJob.Model.T_Base_Student> StuCheckGetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            StuName = "'%" + StuName + "%'";
            SchoolName = "'%" + SchoolName + "%'";
            MajorName = "'%" + MajorName + "%'";
            ClassName = "'%" + ClassName + "%'";

            cm.CommandText = "select top " + PageSize + " * from  T_Base_StudentCheck where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from T_Base_StudentCheck where ischeck=1 and Name like " + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName + ") and  Name like " + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and ischeck = 0 and School like " + SchoolName;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_Student> lst = new List<Model.T_Base_Student>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_Student Stu = new Model.T_Base_Student();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                Stu.Id = Convert.ToInt32(dr["Id"]);

                Stu.Name = Convert.ToString(dr["Name"]);
                Stu.Major = Convert.ToString(dr["Major"]);
                Stu.Phone = Convert.ToString(dr["Phone"]);
                Stu.Class = Convert.ToString(dr["Class"]);
                Stu.School = Convert.ToString(dr["School"]);
                Stu.IdCard = Convert.ToString(dr["IdCard"]);
                Stu.UserId = Convert.ToInt32(dr["UserId"]);
                Stu.Gender = Convert.ToString(dr["Gender"]);
                lst.Add(Stu);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int StuCheckCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from T_Base_StudentCheck";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        public int StuCheckPass(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from  T_Base_StudentCheck where id =" + ids;
            SqlDataReader dr = cm.ExecuteReader();
            FindJob.Model.T_Base_Student Stu = new Model.T_Base_Student();
            while (dr.Read())
            {
                Stu.Id = Convert.ToInt32(dr["Id"]);
                Stu.Name = Convert.ToString(dr["Name"]);
                Stu.Major = Convert.ToString(dr["Major"]);
                Stu.Phone = Convert.ToString(dr["Phone"]);
                Stu.Class = Convert.ToString(dr["Class"]);
                Stu.School = Convert.ToString(dr["School"]);
                Stu.IdCard = Convert.ToString(dr["IdCard"]);
                Stu.UserId = Convert.ToInt32(dr["UserId"]);
                Stu.Gender = Convert.ToString(dr["Gender"]);
                Stu.IntentJob = Convert.ToInt32(dr["IntentJob"]);
                Stu.IntentPlace= Convert.ToString(dr["IntentPlace"]);
                Stu.IntentSalary= Convert.ToString(dr["IntentSalary"]);
                Stu.Experience= Convert.ToString(dr["Experience"]);
            }
            dr.Close();
            cm.CommandText = "select * from  T_Base_Student where IdCard='" + Stu.IdCard + "'";
            SqlDataReader dr2 = cm.ExecuteReader();

            if (dr2 != null)
            {
                cm.CommandText = "update T_Base_Student set intentsalary= '" + Stu.IntentSalary + "',intentjob='" + Stu.IntentJob + "',experience='" + Stu.Experience + "',intentplace='" + Stu.IntentPlace + "' where idcard ='" + Stu.IdCard + "'";
            }
            else
            {
                cm.CommandText = "insert into T_Base_Student(major,phone,class,gender,name,school,graduateprove,idcard,intentsalary,intentjob,experience,intentplace,userId) values('"
                    + Stu.Major + "','" + Stu.Phone + "','" + Stu.Class + "','" + Stu.Gender + "','" + Stu.Name + "','" + Stu.School + "','" + Stu.GraduateProve + "','" + Stu.IdCard 
                    + "','" + Stu.IntentSalary + "','" + Stu.IntentJob + "','" + Stu.Experience + "','" + Stu.IntentPlace + "','" + Stu.UserId+ "')";
            }
            dr2.Close();
            cm.ExecuteNonQuery();
            //cm.CommandText = "delete from t_stock_inItems where headid in (" + ids + ");delete from t_stock_inhead where id in (" + ids + ") ; ";
            cm.CommandText = "delete from T_Base_StudentCheck where id in (" + ids + ") ; ";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();

            co.Close();
            return result;

        }
        /// <summary>
        /// 学生就业信息审核
        /// </summary>
 
        public List<FindJob.Model.T_Base_EI> EICheckGetList(int CurrentPage, int PageSize, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            StuName = "'%" + StuName + "%'";
            SchoolName = "'%" + SchoolName + "%'";
            MajorName = "'%" + MajorName + "%'";
            ClassName = "'%" + ClassName + "%'";
            
            cm.CommandText = "select top " + PageSize + " * from  V_EICheckInfo where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from V_EICheckInfo where stuName like " + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName + ") and  stuName like " + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_EI> lst = new List<Model.T_Base_EI>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_EI item = new Model.T_Base_EI();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                item.Id = Convert.ToInt32(dr["Id"]);

                item.AssociateMajor = Convert.ToBoolean(dr["AssociateMajor"]);
                item.Place = Convert.ToString(dr["Place"]);
                item.Salary = Convert.ToDecimal(dr["Salary"]);
                item.Name = Convert.ToString(dr["Name"]);
                item.SanFang = Convert.ToString(dr["SanFang"]);
                item.StudentId = Convert.ToInt32(dr["StudentId"]);
                item.Major = Convert.ToString(dr["Major"]);
                item.Phone = Convert.ToString(dr["Phone"]);
                item.Class = Convert.ToString(dr["Class"]);
                item.School = Convert.ToString(dr["School"]);
                item.StuName = Convert.ToString(dr["StuName"]);
                lst.Add(item);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int EICheckCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from V_EICheckInfo";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        public int EICheckPass(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from  T_Base_EICheck where id =" + ids;
            SqlDataReader dr = cm.ExecuteReader();
            
            FindJob.Model.T_Base_EI item = new Model.T_Base_EI();
            while (dr.Read())
            {
                
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                item.Id = Convert.ToInt32(dr["Id"]);

                item.AssociateMajor = Convert.ToBoolean(dr["AssociateMajor"]);
                item.Place = Convert.ToString(dr["Place"]);
                item.Salary = Convert.ToDecimal(dr["Salary"]);
                item.Name = Convert.ToString(dr["Name"]);
                item.SanFang = Convert.ToString(dr["SanFang"]);
                item.StudentId = Convert.ToInt32(dr["StudentId"]);

               
            }
            dr.Close();
            cm.CommandText = "select * from  T_Base_EI where studentid='" + item.StudentId + "'";
            SqlDataReader dr2 = cm.ExecuteReader();

            if (dr2 != null)
            {
                cm.CommandText = "update T_Base_EI set AssociateMajor= '" + item.AssociateMajor + "',Place='" + item.Place + "',Salary='" + item.Salary + "',Name='" + item.Name + "',SanFang='" + item.SanFang + "'where studentid ='" + item.StudentId + "'";
            }
            else
            {
                cm.CommandText = "insert into T_Base_EI(AssociateMajor,Place,Salary,name,sanfang,studentid) values('"
                    + item.AssociateMajor + "','" + item.Place + "','" + item.Salary + "','" + item.Name + "','" + item.SanFang + "','" + item.StudentId + "')";
            }
            dr2.Close();
            cm.ExecuteNonQuery();
            //cm.CommandText = "delete from t_stock_inItems where headid in (" + ids + ");delete from t_stock_inhead where id in (" + ids + ") ; ";
            cm.CommandText = "delete from T_Base_EICheck where id in (" + ids + ") ; ";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();

            co.Close();
            return result;

        }

        /// <summary>
        /// 学生就业信息
        /// </summary>
        public List<FindJob.Model.T_Base_EI> EIGetList(int PageSize, int CurrentPage, string StuName, string SchoolName, string MajorName, string ClassName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            StuName = "'%" + StuName + "%'";
            SchoolName = "'%" + SchoolName + "%'";
            MajorName = "'%" + MajorName + "%'";
            ClassName = "'%" + ClassName + "%'";

            cm.CommandText = "select top " + PageSize + " * from  V_EIInfo where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from V_EIInfo where stuName like " + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName + ") and  stuName like " + StuName + " and Major like " + MajorName + " and Class like " + ClassName + " and School like " + SchoolName;
            //cm.Parameters.AddWithValue("@pageSize", PageSize);
            //cm.Parameters.AddWithValue("@beforeCount", PageSize * (CurrentPage - 1));
            SqlDataReader dr = cm.ExecuteReader();
            List<FindJob.Model.T_Base_EI> lst = new List<Model.T_Base_EI>();
            while (dr.Read())
            {
                FindJob.Model.T_Base_EI item = new Model.T_Base_EI();
                //FindJob.Model.T_Base_EnterpriseHead head = new Model.T_Base_EnterpriseHead();
                item.Id = Convert.ToInt32(dr["Id"]);

                item.AssociateMajor = Convert.ToBoolean(dr["AssociateMajor"]);
                item.Place = Convert.ToString(dr["Place"]);
                item.Salary = Convert.ToDecimal(dr["Salary"]);
                item.Name = Convert.ToString(dr["Name"]);
                item.SanFang = Convert.ToString(dr["SanFang"]);
                item.StudentId = Convert.ToInt32(dr["StudentId"]);
                item.Major = Convert.ToString(dr["Major"]);
                item.Phone = Convert.ToString(dr["Phone"]);
                item.Class = Convert.ToString(dr["Class"]);
                item.School = Convert.ToString(dr["School"]);
                item.StuName = Convert.ToString(dr["StuName"]);
                lst.Add(item);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int EICount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select count(*) from V_EIInfo";
            object obj = cm.ExecuteScalar();
            co.Close();
            return (int)obj;
        }
        public int EIDelete(string ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = constr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            //cm.CommandText = "delete from t_stock_inItems where headid in (" + ids + ");delete from t_stock_inhead where id in (" + ids + ") ; ";
            cm.CommandText = "delete from T_Base_EI where id in (" + ids + ") ; ";
            cm.Connection = co;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
   
}
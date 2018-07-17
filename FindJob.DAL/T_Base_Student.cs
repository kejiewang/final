using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace FindJob.DAL
{
    public class T_Base_Student
    {
        /// <summary>
        /// 读取数据库连接字符串
        /// </summary>
        public string sqlstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

        /// <summary>
        /// 添加学生认证信息
        /// </summary>
        /// <param name="item"></param>
        public int AddInfoSave(FindJob.Model.T_Base_Student item)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.Parameters.AddWithValue("@Major", item.Major);
            cm.Parameters.AddWithValue("@Phone", item.Phone);
            cm.Parameters.AddWithValue("@Class", item.Class);
            cm.Parameters.AddWithValue("@Gender", item.Gender);
            cm.Parameters.AddWithValue("@Name", item.Name);
            cm.Parameters.AddWithValue("@IsChecked", false);
            cm.Parameters.AddWithValue("@School", item.School);
            cm.Parameters.AddWithValue("@GraduateProve", item.GraduateProve);
            cm.Parameters.AddWithValue("@IdCard", item.IdCard);
            cm.Parameters.AddWithValue("@IntentSalary", item.IntentSalary);
            cm.Parameters.AddWithValue("@IntentJob", item.IntentJob);
            cm.Parameters.AddWithValue("@Experience", item.Experience);
            cm.Parameters.AddWithValue("@IntentPlace", item.IntentPlace);
            cm.Parameters.AddWithValue("@UserId", item.UserId);

            cm.CommandText = "insert into T_Base_Student values(@Major,@Phone,@Class,@Gender,@Name,@School,@GraduateProve,@IdCard,@IntentSalary,@IntentJob,@Experience,@IntentPlace,@UserId)";
            int result = cm.ExecuteNonQuery();
            co.Close();

            return result;
        }

        /// <summary>
        /// 查找对应学生
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public FindJob.Model.T_Base_Student GetModel(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_Student where id=@Id";
            cm.Parameters.AddWithValue("@Id", Id);
            SqlDataReader dr = cm.ExecuteReader();

            FindJob.Model.T_Base_Student item = null;
            while (dr.Read())
            {
                item = new Model.T_Base_Student();
                item.Id = Convert.ToInt32(dr["Id"]);
                item.Major = Convert.ToString(dr["Major"]);
                item.Phone = Convert.ToString(dr["Phone"]);
                item.Class = Convert.ToString(dr["Class"]);
                item.Gender = Convert.ToString(dr["Gender"]);
                item.Name = Convert.ToString(dr["Name"]);
                item.IsCheck = Convert.ToBoolean(dr["IsChecked"]);
                item.School = Convert.ToString(dr["School"]);
                item.GraduateProve = Convert.ToString(dr["GraduateProve"]);
                item.IdCard = Convert.ToString(dr["IdCard"]);
                item.IntentSalary = Convert.ToString(dr["IntentSalary"]);
                item.IntentJob = Convert.ToInt32(dr["IntentJob"]);
                item.Experience = Convert.ToString(dr["Experience"]);
                item.IntentPlace = Convert.ToString(dr["IntentPlace"]);
                item.UserId = Convert.ToInt32(dr["UserId"]);

            }
            dr.Close();
            co.Close();
            return item;
        }

        public int Update(FindJob.Model.T_Base_Student item)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = sqlstr;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;


            //"update into T_Base_Student values(@Major,@Phone,@Class,@Gender,@Name,@School,@GraduateProve,@IdCard,@IntentSalary,@IntentJob,@Experience,@IntentPlace,@UserId)";
            cm.CommandText = cm.CommandText = "insert into T_Base_StudentCheck values(@Major,@Phone,@Class,@Gender,@Name,@School,@GraduateProve,@IdCard,@IntentSalary,@IntentJob,@Experience,@IntentPlace,@UserId)";
            cm.Parameters.AddWithValue("@Major", item.Major);
            cm.Parameters.AddWithValue("@Phone", item.Phone);
            cm.Parameters.AddWithValue("@Class", item.Class);
            cm.Parameters.AddWithValue("@Gender", item.Gender);
            cm.Parameters.AddWithValue("@Name", item.Name);
            cm.Parameters.AddWithValue("@IsChecked", false);
            cm.Parameters.AddWithValue("@School", item.School);
            cm.Parameters.AddWithValue("@GraduateProve", item.GraduateProve);
            cm.Parameters.AddWithValue("@IdCard", item.IdCard);
            cm.Parameters.AddWithValue("@IntentSalary", item.IntentSalary);
            cm.Parameters.AddWithValue("@IntentJob", item.IntentJob);
            cm.Parameters.AddWithValue("@Experience", item.Experience);
            cm.Parameters.AddWithValue("@IntentPlace", item.IntentPlace);
            cm.Parameters.AddWithValue("@UserId", item.UserId);
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

    }

    
}

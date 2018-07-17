using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FindJob.Model;

namespace FindJob.DAL
{
    public class T_Base_User
    {

        public string connstring = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

        public int RegisterSave(FindJob.Model.T_Base_User item)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.Parameters.AddWithValue("@LoginName",item.LoginName);
            cm.Parameters.AddWithValue("@Password",item.Password);
            cm.Parameters.AddWithValue("@Phone",item.Phone);
            cm.Parameters.AddWithValue("@RoleId",item.RoleId);
            cm.Parameters.AddWithValue("@Pic", item.Pic);
            cm.CommandText = "insert into T_Base_User (LoginName,Password, RoleId, Phone, Pic) values(@LoginName,@Password,@RoleId,@Phone,@Pic)";
            int result = cm.ExecuteNonQuery();


            co.Close();
            return result;
        }

        public void Update(Model.T_Base_User item)
        {
            //throw new NotImplementedException();
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "update t_base_user set password=@Password, Phone=@Phone, Pic = @Pic where Id = @Id";
            cm.Parameters.AddWithValue("@Password", item.Password);
            cm.Parameters.AddWithValue("@Phone", item.Phone);
            cm.Parameters.AddWithValue("@Pic", item.Pic);
            cm.Parameters.AddWithValue("@Id", item.Id);
            cm.ExecuteNonQuery();
            co.Close();
        }

        public Model.T_Base_User Find(string loginName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_User where LoginName = @LoginName";
            cm.Parameters.AddWithValue("@LoginName", loginName);
            SqlDataReader dr = cm.ExecuteReader();
            FindJob.Model.T_Base_User item = new Model.T_Base_User();
            item.LoginName = loginName;
            item.Pic = "/assets/img/ui-sam.jpg";
            while (dr.Read())
            {
                item.Id = Convert.ToInt32(dr["Id"]);
                item.LoginName = Convert.ToString(dr["LoginName"]);
                item.Password = Convert.ToString(dr["Password"]);
                item.Phone = Convert.ToString(dr["Phone"]);
                item.RoleId = Convert.ToInt32(dr["RoleId"]);
                item.Pic = Convert.ToString(dr["Pic"]);
            }
            dr.Close();
            co.Close();
            if (item.Pic.Equals(""))
            {
                item.Pic = "/assets/img/ui-sam.jpg";
            }
            return item;
        }

        public Model.T_Base_User Find(int id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_User where Id = @Id";
            cm.Parameters.AddWithValue("@Id", id);
            SqlDataReader dr = cm.ExecuteReader();
            FindJob.Model.T_Base_User item = new Model.T_Base_User();
            item.Pic = "/assets/img/ui-sam.jpg";
            while (dr.Read())
            {
                item.Id = Convert.ToInt32(dr["Id"]);
                item.LoginName = Convert.ToString(dr["LoginName"]);
                item.Password = Convert.ToString(dr["Password"]);
                item.Phone = Convert.ToString(dr["Phone"]);
                item.RoleId = Convert.ToInt32(dr["RoleId"]);
                item.Pic = Convert.ToString(dr["Pic"]);
            }
            if(item.Pic.Equals("")) {
                item.Pic = "/assets/img/ui-sam.jpg";
            }
            dr.Close();
            co.Close();

            return item;
        }

        public Model.T_Base_User Check(string loginName, string pWD)
        {
            

            SqlConnection co = new SqlConnection();
            co.ConnectionString = connstring;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from T_Base_Admin where LoginName = @LoginName and Password = @PWD";
            cm.Parameters.AddWithValue("@LoginName", loginName);
            cm.Parameters.AddWithValue("@PWD", pWD);
            SqlDataReader dr = cm.ExecuteReader();
            FindJob.Model.T_Base_User admin = new Model.T_Base_User();
            admin.LoginName = "-1";
            admin.Password = "-1";
            while (dr.Read())
            {
                admin.LoginName = Convert.ToString(dr["LoginName"]);
                admin.Password = Convert.ToString(dr["Password"]);
                admin.RoleId = Convert.ToInt32(dr["RoleId"]);
                admin.Phone = Convert.ToString(dr["Phone"]);
            }

            dr.Close();
            co.Close();
            return admin;
        }
    }
}

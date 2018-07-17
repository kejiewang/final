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
            cm.CommandText = "insert into T_Base_User values(@LoginName,@Password,@RoleId,@Phone)";
            int result = cm.ExecuteNonQuery();


            co.Close();
            return result;
        }

    }
}

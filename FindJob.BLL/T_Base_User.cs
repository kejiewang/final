using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindJob.Model;

namespace FindJob.BLL
{
    public class T_Base_User
    {
         public int RegisterSave(FindJob.Model.T_Base_User item)
        {
            return new FindJob.DAL.T_Base_User().RegisterSave(item);
        }

        public Model.T_Base_User Check(string loginName, string pWD)
        {
            return new FindJob.DAL.T_Base_User().Check(loginName, pWD);
        }

        public Model.T_Base_User Find(int id)
        {
            FindJob.DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.Find(id);
        }

        public Model.T_Base_User Find(string LoginName)
        {
            FindJob.DAL.T_Base_User dal = new DAL.T_Base_User();
            return dal.Find(LoginName);
        }

        public void Update(Model.T_Base_User item)
        {
            //throw new NotImplementedException();

            FindJob.DAL.T_Base_User dal = new DAL.T_Base_User();
            dal.Update(item);
        }
    }
}

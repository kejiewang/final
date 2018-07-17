using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.BLL
{
    public class T_Base_User
    {
         public int RegisterSave(FindJob.Model.T_Base_User item)
        {
            return new FindJob.DAL.T_Base_User().RegisterSave(item);
        }
    }
}

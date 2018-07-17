using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.BLL
{
    public class T_Base_Enterpirse
    {
        public int AddInfoSave(FindJob.Model.T_Base_Enterprise enterprise)
        {
            return new FindJob.DAL.T_Base_Enterprise().AddInfoSave(enterprise);
        }

        public FindJob.Model.T_Base_Enterprise GetModel(int Id)
        {

            FindJob.Model.T_Base_Enterprise enterprise = new Model.T_Base_Enterprise();
            enterprise = (new FindJob.DAL.T_Base_Enterprise()).GetModel(Id);

            return enterprise;
        }
        public int Update(FindJob.Model.T_Base_Enterprise enterprise)
        {
            FindJob.DAL.T_Base_Enterprise dal = new DAL.T_Base_Enterprise();

            int result = dal.Update(enterprise);

            return result;

        }

        public Model.T_Relation_RecruitInfo GetRecruitInfo(int EPId, string JobName)
        {
            return new DAL.T_Base_Enterprise().GetRecruitInfo(EPId, JobName);
        }

    }
}
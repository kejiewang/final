using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindJob.Model;

namespace FindJob.BLL
{

    public class T_Base_Studet
    {

        public int AddInfoSave(FindJob.Model.T_Base_Student item)
        {
            return new FindJob.DAL.T_Base_Student().AddInfoSave(item);
        }

        public FindJob.Model.T_Base_Student GetModel(int Id)
        {

            FindJob.Model.T_Base_Student item = new Model.T_Base_Student();
            item = (new FindJob.DAL.T_Base_Student()).GetModel(Id);

            return item;
        }
        public int Update(FindJob.Model.T_Base_Student item)
        {
            FindJob.DAL.T_Base_Student dal = new DAL.T_Base_Student();

            int result = dal.Update(item);

            return result;

        }

        public void EISubmitSave(T_Base_EI eI)
        {
             new DAL.T_Base_Student().EISubmitSave(eI);
        }

        public List<Model.T_Relation_RecruitInfo> GetRecruitInfoList(int EPId) {
            return new DAL.T_Base_Enterprise().GetRecruitInfoList(EPId);
        }

        public int ApplyJob(int userId, int EPId, string JobName)
        {
            FindJob.DAL.T_Base_Student dal = new DAL.T_Base_Student();

            int result = dal.ApplyJob(userId,EPId,JobName);

            return result;

        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
   public class T_Relation_RecruitInfo
    {
        public int Id { get; set; }
        public Boolean IsChecked { get; set; }
        public string WorkPlace { get; set; }
        public string Salary { get; set; }
        public string Requirement { get; set; }
        public int Amount { get; set; }
        public int JobId { get; set; }
        public int EnterpriseId { get; set; }
    }
}

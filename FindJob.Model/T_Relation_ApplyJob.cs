using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
    public class T_Relation_ApplyJob
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public string JobName { get; set; }
        public int StudentId { get; set; }
        /// <summary>
        /// 学生部分信息
        /// </summary>
        public string Major { get; set; }  
        public string Class { get; set; }
        public string StuName { get; set; }
        public string School { get; set; }
        /// <summary>
        /// 企业部分信息
        /// </summary>
        public string EPName { get; set; }
        public string EPTel { get; set; }
        /// <summary>
        /// 工作部分信息
        /// </summary>
        public string WorkPlace { get; set; }
        public string Salary { get; set; }
    }
  
}

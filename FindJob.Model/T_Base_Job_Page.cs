using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
    public class T_Base_Job_Page
    {
        public List<T_Base_Job> jobList { get; set; }

        public int count { get; set; }//自动生成属性的方法，prop+tap+tap
    }
}

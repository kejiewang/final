using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
    public class T_Base_Enterprise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Introduction { get; set; }
        public string Qualification { get; set; }
        public Boolean IsChecked { get; set; }
        public int UserId { get; set; }
    }
}

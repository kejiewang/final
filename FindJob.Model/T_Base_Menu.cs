using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
    public class T_Base_Menu
    {

        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Display { get; set; }
        public string Type { get; set; }
        public string ParentId { get; set; }

    }
}

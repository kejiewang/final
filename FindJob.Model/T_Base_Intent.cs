using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
    public class T_Base_Intent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string IntentPlace { get; set; }
        public decimal Money { get; set; }
        public int IntentJob { get; set; }
    }
}

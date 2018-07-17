using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindJob.Model
{
    public class Message
    {
        /// <summary>
        /// 消息返回代码 200代表成功
        /// </summary>
        public int Code { get; set; }
        public string Content { get; set; }
    }
}

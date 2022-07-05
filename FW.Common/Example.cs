using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common
{
    public class Example
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime createtime { get; set; }

        public Example Init()
        {
            Example sdt = new Example { Id = "3", Name = "3abc", Age = 310, createtime = DateTime.Now };
            return sdt;
        }
    }
}

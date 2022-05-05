using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.Common
{
    public class TestHelper
    {
        public static void GetStr() {
            int aa = default;
            string bb = default;
            SetValue();
            Console.WriteLine(aa);
            void SetValue() {
                aa = Thread.CurrentThread.ManagedThreadId;
                bb = "22";
            };
            
        }
    }
}

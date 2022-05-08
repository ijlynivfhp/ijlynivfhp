using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
[assembly: CompilationRelaxationsAttribute(CompilationRelaxations.NoStringInterning)]

namespace FW.Common
{
    public class TestHelper
    {
        private static readonly object lockStr = new object();
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

        public void DoJob(string i)
        {
            int a = 1;
            int b = a;
            var c= object.ReferenceEquals(a, b);
            var aa = "11";
            var bb = "11";
            var cc = object.ReferenceEquals(aa,bb);

            var aaa = "11" + 1;
            var bbb = "11" + 1;
            var ccc=object.ReferenceEquals(aaa, bbb);
            var aaaa = new TestA() { AAAA = "1111", BBBB = 1111 };
            //aaaa.AAAA += 222222;
            string dd = aaaa.AAAA;
            lock(dd)
            {
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                Thread.Sleep(1000);
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine(i);
            }
        }

        public void DoJobA(string i)
        {
            int a = 1;
            int b = 1;
            var c = object.ReferenceEquals(a, b);
            var aa = "11";
            var bb = "11";
            var cc = object.ReferenceEquals(aa, bb);

            var aaa = "11" + 1;
            var bbb = "11" + 1;
            var ccc = object.ReferenceEquals(aaa, bbb);


            var aaaa = new TestA() { AAAA = "1111", BBBB = 1111 };

            int dd = aaaa.BBBB;
            lock (dd.ToString())
            {
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                Thread.Sleep(1000);
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine("000000000000000000000");
                System.Console.WriteLine(i);
            }
        }
    }
}

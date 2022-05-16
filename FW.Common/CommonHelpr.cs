using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.Common
{
    public class CommonHelpr
    {
        private static readonly object lockObjA = new object();
        private static readonly object lockObjB = new object();
        public static async Task GetStr()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("https://www.lmlphp.com/user/151066/article/item/3803774/"))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    //streamToReadFrom.Position = 0;
                    //StreamReader reader = new StreamReader(streamToReadFrom);
                    //string text = reader.ReadToEnd();
                    System.Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                }
            }
        }

        public static void TestA()
        {
            lock (lockObjA) {
                int aa = 0;
                aa = 0;
                aa = 0;
                aa = 0;
                aa = 0;
                aa = 0;
                Thread.Sleep(1000);
                lock (lockObjB) {
                    int bb = 0;
                    bb = 0;
                    bb = 0;
                    bb = 0;
                    bb = 0;
                    bb = 0;
                }
            }
        }
        public static void TestB()
        {
            lock (lockObjB)
            {
                int aa = 0;
                aa = 0;
                aa = 0;
                aa = 0;
                aa = 0;
                aa = 0;
                Thread.Sleep(1000);
                lock (lockObjA)
                {
                    int bb = 0;
                    bb = 0;
                    bb = 0;
                    bb = 0;
                    bb = 0;
                    bb = 0;
                }
            }
        }
    }
}

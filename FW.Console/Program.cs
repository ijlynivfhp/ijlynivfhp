using FW.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.Console
{
    internal class Program
    {
        private static int TotalCount;
        static void Main(string[] args)
        {
            int aaa = Environment.ProcessorCount;
            int minWorker, minIOC;
            // Get the current settings.
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            var tt = ThreadPool.SetMaxThreads(0, 0);
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            ThreadPool.GetAvailableThreads(out int workerCount,out int cpCount);
            System.Console.WriteLine(workerCount); System.Console.WriteLine(cpCount);
            
            var aa= Task.Factory.StartNew(() => {
                CommonHelpr.TestA();
            });
            var bb= Task.Factory.StartNew(() => {
                CommonHelpr.TestB();
            });
            Task.WaitAll(new List<Task>(){ aa,bb }.ToArray());
            ThreadPool.GetAvailableThreads(out int workerCount1, out int cpCount1);
            System.Console.WriteLine(1111111111);
            //System.Console.WriteLine($"abc{Thread.CurrentThread.ManagedThreadId}");
            //for (int i = 0; i < 20; i++)
            //{
            //    CommonHelpr.GetStr();
            //}
            //System.Console.WriteLine($"abc{Thread.CurrentThread.ManagedThreadId}");
            //var bb = new { aa = "", tt = 2 };
            //int aa = Environment.ProcessorCount;
            //int minWorker, minIOC;
            //// Get the current settings.
            //ThreadPool.GetMinThreads(out minWorker, out minIOC);
            //var tt= ThreadPool.SetMaxThreads(0,0);
            //ThreadPool.GetMinThreads(out minWorker, out minIOC);
            //for (int i = 0; i < 30; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(o=>DoJob());
            //}



            //ThreadPool.SetMaxThreads(100);
            //var error = string.Empty;
            //new RabbitMqFMTwoHelper(new RabbitMqConfigModel());
            ////RabbitMqFMTwoHelper.Send("000000000000", ref error);
            //Func<string, bool> func = new Func<string, bool>(Say);
            //RabbitMqFMTwoHelper.Receive<string>(func);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 110, FW.Common.Common.QueueNamePre + 110 + 1111);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 110, FW.Common.Common.QueueNamePre + 110 + 2222);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 220, FW.Common.Common.QueueNamePre + 220 + 1111);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 220, FW.Common.Common.QueueNamePre + 220 + 2222);
            //new RabbitMqReceiveHelper().StartReceiveMsg(FW.Common.Common.QueueNamePre, FW.Common.Common.RoutingKeyName+110);
            //new RabbitMqReceiveHelper().StartReceiveMsg(FW.Common.Common.QueueNamePre, FW.Common.Common.RoutingKeyName+220);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre, FW.Common.Common.QueueNamePre);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 110, FW.Common.Common.QueueNamePre + "." + 110 + "." + 2222);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 220, FW.Common.Common.QueueNamePre + "." + 220 + "." + 1111);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 220, FW.Common.Common.QueueNamePre + "." + 220 + "." + 2222);
            while (true) { 
                System.Console.ReadLine();
            }
        }
        static void DoJob()
        {
            TotalCount++;
            System.Console.WriteLine(TotalCount);
            while (true) {
            }
        }
    }
}

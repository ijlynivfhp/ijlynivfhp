using FW.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            var startDirectory = @"F:\";
            var w = Stopwatch.StartNew();
            ThisIsARecursiveFunction(startDirectory);
            System.Console.WriteLine("Elapsed seconds: " + w.Elapsed.TotalSeconds);
            System.Console.ReadKey();
        }
        public static void ThisIsARecursiveFunction(String currentDirectory)
        {
            var lastBit = Path.GetFileName(currentDirectory);
            var depth = currentDirectory.Count(t => t == '\\');
            //Console.WriteLine(depth + ": " + currentDirectory);
            try
            {
                var children = Directory.GetDirectories(currentDirectory);
                //Edit this mode to switch what way of parallelization it should use
                int mode = 2;
                switch (mode)
                {
                    case 1:
                        foreach (var child in children)
                        {
                            ThisIsARecursiveFunction(child);
                        }
                        break;
                    case 2:
                        children.AsParallel().ForAll(t =>
                        {
                            ThisIsARecursiveFunction(t);
                        });
                        break;
                    case 3:
                        Parallel.ForEach(children, t =>
                        {
                            ThisIsARecursiveFunction(t);
                        });
                        break;
                    default:
                        break;
                }
            }
            catch (Exception eee)
            {
                //Exception might occur for directories that can't be accessed.
            }



            //var aa= Task.Factory.StartNew(() => {
            //    CommonHelpr.TestA();
            //});
            //var bb= Task.Factory.StartNew(() => {
            //    CommonHelpr.TestB();
            //});
            //Task.WaitAll(new List<Task>(){ aa,bb }.ToArray());
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
        }
        static void DoJob()
        {
            TotalCount++;
            System.Console.WriteLine(TotalCount);
            while (true) {
            }
        }

        public static void Dosome()
        {
            System.Console.WriteLine($"Task Start ThreadId：{Thread.CurrentThread.ManagedThreadId}，DateTime：{DateTime.Now.ToLongTimeString()}");
            Thread.Sleep(5 * 1000); // 模拟任务耗时
            System.Console.WriteLine($"Task End ThreadId：{Thread.CurrentThread.ManagedThreadId}，DateTime：{DateTime.Now.ToLongTimeString()}");
        }
    }
}

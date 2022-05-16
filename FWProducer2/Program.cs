using FW.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWProducer2
{
    internal class Program
    {
        private static System.Timers.Timer timer;

        private static int index = default;

        static RabbitMQ2Helper helper;

        static void Main(string[] args)
        {
            helper = new RabbitMQ2Helper("demoexchange", "demoqueue", ExchangeType.Direct, "demoqueue", "sa", "123456", "127.0.0.1", 5672, "vhost");
            helper.MQMsg += Helper_MQMsg;
            helper.MQError += Helper_MQError;

            timer = new System.Timers.Timer(1);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            helper.Producer();
        }

        private static void Helper_MQMsg(string msg)
        {
            Console.WriteLine("已发送： {0}", msg);
        }
        private static void Helper_MQError(string error)
        {
            Console.WriteLine("错误信息： {0}", error);
        }
        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int i = 7;
            while (i > 0)
            {
                helper.SendMsg($"ABC{index++}");
                i--;
            }
        }
    }
}

using FW.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWConsumer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Consumer();
            RabbitMQ2Helper helper = new RabbitMQ2Helper("demoexchange", "demoqueue", ExchangeType.Direct, "demoqueue", "sa", "123456", "127.0.0.1", 5672, "vhost");
            helper.MQMsg += Helper_MQMsg;
            helper.MQError += Helper_MQError;
            helper.ConsumerExtra();
        }

        private static void Helper_MQError(string error)
        {
            Console.WriteLine("错误信息： {0}", error);
        }

        private static void Helper_MQMsg(string msg)
        {
            Console.WriteLine("已接收： {0}", msg);
        }
    }
}

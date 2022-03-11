using FW.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var error = string.Empty;
            //new RabbitMqFMTwoHelper(new RabbitMqConfigModel());
            ////RabbitMqFMTwoHelper.Send("000000000000", ref error);
            //Func<string, bool> func = new Func<string, bool>(Say);
            //RabbitMqFMTwoHelper.Receive<string>(func);
            new RabbmitMqSendHelper().SendMsg("111111111111111");
            var bb= new RabbitMqReceiveHelper().StartReceiveMsg();
        }
        static bool Say(string msg) {
            System.Console.WriteLine(msg);
            return true;
        }
	}
}

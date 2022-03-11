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
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 110, FW.Common.Common.QueueNamePre + 110 + 1111);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 110, FW.Common.Common.QueueNamePre + 110 + 2222);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 220, FW.Common.Common.QueueNamePre + 220 + 1111);
            //new RabbmitMqSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + 220, FW.Common.Common.QueueNamePre + 220 + 2222);
            //new RabbitMqReceiveHelper().StartReceiveMsg(FW.Common.Common.QueueNamePre, FW.Common.Common.RoutingKeyName+110);
            //new RabbitMqReceiveHelper().StartReceiveMsg(FW.Common.Common.QueueNamePre, FW.Common.Common.RoutingKeyName+220);
            new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 110, FW.Common.Common.QueueNamePre + "." + 110 + "." + 1111);
            new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 110, FW.Common.Common.QueueNamePre + "." + 110 + "." + 2222);
            new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 220, FW.Common.Common.QueueNamePre + "." + 220 + "." + 1111);
            new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 220, FW.Common.Common.QueueNamePre + "." + 220 + "." + 2222);
            System.Console.ReadLine();
        }
        static bool Say(string msg) {
            System.Console.WriteLine(msg);
            return true;
        }
	}
}

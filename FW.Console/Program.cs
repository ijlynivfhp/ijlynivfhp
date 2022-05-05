using FW.Common;
using Newtonsoft.Json;
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
            dynamic bb = new System.Dynamic.ExpandoObject();
            bb.aa = "123";
            bb.bb = 456;
            if (string.IsNullOrEmpty((string)bb.cc))
            {
                System.Console.WriteLine(111111111111);
            }

            //var bb = new { aa = "", tt = 2 };
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
            //new RabbitMQTTTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre, FW.Common.Common.QueueNamePre);
            //RabbitMQTTTopicSendHelper.SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre, FW.Common.Common.QueueNamePre);

            //RabbitMQTTTopicReceiveHelper.StartReceiveMsg(FW.Common.Common.QueueNamePre, FW.Common.Common.QueueNamePre);
            //TestHelper.GetStr();
            List<int> aa = new List<int>() { 1,2,3};
            List<int> ee = new List<int>() { 3, 4, 5 };
            aa.AddRange(bb);
            //dynamic aa =new System.Dynamic.ExpandoObject();
            //aa.aa = null;
            //aa.bb = 0;
            //var tt = JsonConvert.SerializeObject(aa);
            //for (int i = 0; i < 10; i++)
            //{
            //    Task.Factory.StartNew(() => TestHelper.GetStr());
            //}
            //RabbitMQTTTopicReceiveHelper.StartReceiveMsgNew(FW.Common.Common.QueueNamePre, FW.Common.Common.QueueNamePre);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 110, FW.Common.Common.QueueNamePre + "." + 110 + "." + 2222);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 220, FW.Common.Common.QueueNamePre + "." + 220 + "." + 1111);
            //new RabbitMqTopicSendHelper().SendMsg<dynamic>(new { aa = 11, bb = 22 }, FW.Common.Common.QueueNamePre + "." + 220, FW.Common.Common.QueueNamePre + "." + 220 + "." + 2222);
            #region
            do
            {
                System.Console.ReadLine();
            } while (true);
            #endregion

        }
    }
}

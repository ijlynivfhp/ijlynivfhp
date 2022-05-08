using FW.Common;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
            var conn = RabbitMqHelper.GetConnection();
            var channel = conn.CreateModel();
            var exchangeName = "DelayExchange";
            var routingkey = "Delay.Test";
            var queueName = "DelayQueueName";
            try
            {
                //设置Exchange队列类型
                var argMaps = new Dictionary<string, object>(){{"x-delayed-type", "topic"}};
                //设置当前消息为延时队列
                channel.ExchangeDeclare(exchange: exchangeName, type: "x-delayed-message", true, false, argMaps);
                channel.QueueDeclare(queueName, true, false, false, argMaps);
                channel.QueueBind(queueName, exchangeName, routingkey);
                for (int i = 0; i < 3; i++)
                {
                    var time = 1000 * 10;
                    var message = $@"发送时间为 {DateTime.Now:yyyy-MM-dd HH:mm:ss} 延时时间为:{time}";
                    var body = Encoding.UTF8.GetBytes(message);
                    var props = channel.CreateBasicProperties();
                    //设置消息的过期时间
                    props.Headers = new Dictionary<string, object>() { { "x-delay", 10000 } };
                    channel.BasicPublish(exchange: exchangeName,
                        routingKey: routingkey,
                        basicProperties: props,
                        body: body);
                    System.Console.WriteLine(message);
                }
            }
            finally {
                channel?.Close();
                conn?.Close();
            }


            conn = RabbitMqHelper.GetConnection();
            channel = conn.CreateModel();
            channel.QueueDeclare(queueName, true, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var bodyMsg = ea.Body;
                var message = Encoding.UTF8.GetString(bodyMsg.ToArray());
                var routingKey = ea.RoutingKey;
                System.Console.WriteLine($@"接受到消息的时间为 {DateTime.Now:yyyy-MM-dd HH:mm:ss},routingKey:{routingKey} message:{message} ");
                channel.BasicAck(ea.DeliveryTag, false);
            };
            //进行消费
            channel.BasicConsume(queueName, false, consumer);
            System.Console.ReadLine();

            //for (int i = 0; i < 100; i++)
            //{
            //    Task.Factory.StartNew(() =>
            //    {
            //        new TestHelper().DoJob($"aaaaaaaaaaaaaaaaa{i}");
            //    });

            //    //Task.Factory.StartNew(() =>
            //    //{
            //    //    new TestHelper().DoJobA($"aaaaaaaaaaaaaaaaa{i}");
            //    //});
            //}

            //Task task1 = Task.Factory.StartNew(async () =>
            //{
            //    System.Console.WriteLine($" Task.Delay 有await 开始：{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //    for (int i = 0; i < 10; i++)
            //    {
            //        System.Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} 有await正在进行：{i}");
            //        await Task.Delay(2000);
            //        System.Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} 正在进行：{i * 10}");
            //    }
            //    System.Console.WriteLine($" Task.Delay 有await 结束：{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //    System.Console.ReadKey();
            //});
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
            List<int> tt = new List<int>() { 1,2,3};
            List<int> yy = new List<int>() { 3, 4, 5 };
            tt.AddRange(yy);
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

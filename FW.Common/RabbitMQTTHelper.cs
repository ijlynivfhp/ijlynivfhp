using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace FW.Common
{
    public class RabbitMQTTHelper
    {

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Common.HostName,
                    Port = Common.Port,
                    UserName = Common.UserName,
                    Password = Common.Password,
                    VirtualHost = Common.VirtualHost,
                    Uri = new Uri("mqtt://127.0.0.1"),
                };
                var conn = factory.CreateConnection();
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

    /// <summary>
    /// fanout类型交换机，发送消息
    /// </summary>
    public class RabbitMQTTTopicSendHelper
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SendMsg<T>(T data, string queueName, string routingKeyName, string exchangeName = Common.ExchangeName)
        {
            try
            {
                //using (var conn = RabbitMQTTHelper.GetConnection())
                //{
                //    using (var channel = conn.CreateModel())
                //    {
                //        channel.QueueDeclare(queue: queueName,
                //                     durable: true,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: default);
                //        channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Topic, durable: true);
                //        channel.QueueBind(queueName, exchangeName, "");

                //        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

                //        channel.BasicPublish(exchange: exchangeName,
                //                             routingKey: routingKeyName,
                //                             basicProperties: default,
                //                             body: body);

                //        //Console.WriteLine(" [x] Sent {0}", message);
                //    };
                //};

                // MQTT的主题 topic
                string topic = "/mqtt/test";
                // create client instance 
                MqttClient client = new MqttClient("127.0.0.1");

                string clientId = Guid.NewGuid().ToString();
                client.Connect("54af4bcd-c9ba-47a5-85a7-6ae0650ceec7qos0", "sa","123456");

                client.Publish(topic, Encoding.UTF8.GetBytes("8888888888"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, true);

                Console.WriteLine("Publish!!!");
                
                client.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary>
    /// fanout类型交换机接收消息
    /// </summary>
    public class RabbitMQTTTopicReceiveHelper
    {
        //public RabbitMqReceiveEventHandler OnReceiveEvent;

        private IConnection conn;

        private IModel channel;

        private EventingBasicConsumer consumer;

        public static bool StartReceiveMsg(string queueName, string routingKeyName, string exchangeName = Common.ExchangeName)
        {
            try
            {
                //conn = RabbitMQTTHelper.GetConnection();

                //channel = conn.CreateModel();
                //channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Topic, durable: true);
                ////此处随机取出交换机下的队列
                ////var queueName = channel.QueueDeclare().QueueName;
                //channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKeyName);
                //consumer = new EventingBasicConsumer(channel);
                //consumer.Received += (model, ea) =>
                //{
                //    var body = ea.Body.ToArray();
                //    var message = Encoding.UTF8.GetString(body);
                //    //Console.WriteLine(" [x] Received {0}", message);
                //    //if (OnReceiveEvent != null)
                //    //{
                //    //    OnReceiveEvent(queueName + "::" + message);
                //    //}
                //};
                //channel.BasicConsume(queue: queueName,
                //                        autoAck: true,
                //                        consumer: consumer);

                // MQTT的主题 topic
                string topic = "/mqtt/test";
                // create client instance 
                MqttClient client = new MqttClient("127.0.0.1");

                

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId, "sa", "123456");

                // register to message received 
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = System.Text.Encoding.Default.GetString(e.Message);
            Console.WriteLine(msg);
        }

        public static bool StartReceiveMsgNew(string queueName, string routingKeyName, string exchangeName = Common.ExchangeName)
        {
            try
            {
                //conn = RabbitMQTTHelper.GetConnection();

                //channel = conn.CreateModel();
                //channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Topic, durable: true);
                ////此处随机取出交换机下的队列
                ////var queueName = channel.QueueDeclare().QueueName;
                //channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKeyName);
                //consumer = new EventingBasicConsumer(channel);
                //consumer.Received += (model, ea) =>
                //{
                //    var body = ea.Body.ToArray();
                //    var message = Encoding.UTF8.GetString(body);
                //    //Console.WriteLine(" [x] Received {0}", message);
                //    //if (OnReceiveEvent != null)
                //    //{
                //    //    OnReceiveEvent(queueName + "::" + message);
                //    //}
                //};
                //channel.BasicConsume(queue: queueName,
                //                        autoAck: true,
                //                        consumer: consumer);

                // MQTT的主题 topic
                string topic = "/3a10ec41687f4f69bbf14957c34cef56/P114101/202203010085";
                // create client instance 
                MqttClient client = new MqttClient("iot.walkthink.com");



                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId, "3a10ec41687f4f69bbf14957c34cef56", "58a357db3a5b35dba5fca649491b0131");

                // register to message received 
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceivedNew;

                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void client_MqttMsgPublishReceivedNew(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = System.Text.Encoding.Default.GetString(e.Message);
            Console.WriteLine(msg);
        }
    }
}

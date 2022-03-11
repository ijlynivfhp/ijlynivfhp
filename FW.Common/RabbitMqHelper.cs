using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common
{
    public class RabbitMqHelper
    {

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Common.HostName,
                    Port = Common.Port,
                    UserName = Common.UserName,
                    Password = Common.Password,
                    VirtualHost = Common.VirtualHost
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

    public class RabbmitMqSendHelper : RabbitMqHelper
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendMsg(string msg)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (var channel = conn.CreateModel())
                    {
                        channel.QueueDeclare(queue: Common.QueueNamePre,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: default);
                        channel.ExchangeDeclare(Common.ExchangeName, ExchangeType.Direct, true);
                        channel.QueueBind(Common.QueueNamePre, Common.ExchangeName, Common.RoutingKeyName);

                        var body = Encoding.UTF8.GetBytes(msg);

                        channel.BasicPublish(exchange: Common.ExchangeName,
                                             routingKey: Common.RoutingKeyName,
                                             basicProperties: default,
                                             body: body);

                        //Console.WriteLine(" [x] Sent {0}", message);
                    };
                };
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class RabbitMqReceiveHelper : RabbitMqHelper
    {
        //public RabbitMqReceiveEventHandler OnReceiveEvent;

        private IConnection conn;

        private IModel channel;

        private EventingBasicConsumer consumer;

        public bool StartReceiveMsg()
        {
            try
            {
                conn = GetConnection();

                channel = conn.CreateModel();

                channel.QueueDeclare(queue: Common.QueueNamePre,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: default);

                channel.BasicQos(0, 1, false);//公平分发、同一时间只处理一个消息。

                consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    //Console.WriteLine(" [x] Received {0}", message);
                    //if (OnReceiveEvent != null)
                    //{
                    //    OnReceiveEvent(message);
                    //}
                };
                channel.BasicConsume(queue: Common.QueueNamePre,
                                        autoAck: true,
                                        consumer: consumer);
                return true;
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
    public class RabbitMqFanoutSendHelper : RabbitMqHelper
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendMsg(string msg)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    using (var channel = conn.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "amq.fanout", type: ExchangeType.Fanout, durable: true);

                        var body = Encoding.UTF8.GetBytes(msg);

                        channel.BasicPublish(exchange: "amq.fanout",
                                             routingKey: "",
                                             basicProperties: null,
                                             body: body);

                        //Console.WriteLine(" [x] Sent {0}", message);
                    };
                };
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
    public class RabbitMqFanoutReceiveHelper : RabbitMqHelper
    {
        //public RabbitMqReceiveEventHandler OnReceiveEvent;

        private IConnection conn;

        private IModel channel;

        private EventingBasicConsumer consumer;

        public bool StartReceiveMsg(string queueName)
        {
            try
            {
                conn = GetConnection();

                channel = conn.CreateModel();
                channel.ExchangeDeclare(exchange: "amq.fanout", type: ExchangeType.Fanout, durable: true);
                //此处随机取出交换机下的队列
                //var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "amq.fanout", routingKey: "");
                consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    //Console.WriteLine(" [x] Received {0}", message);
                    //if (OnReceiveEvent != null)
                    //{
                    //    OnReceiveEvent(queueName + "::" + message);
                    //}
                };
                channel.BasicConsume(queue: queueName,
                                        autoAck: true,
                                        consumer: consumer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Common {
        public const string VirtualHost = "/";
        public const string HostName = "127.0.0.1";
        public const int Port = 5672;
        public const string UserName = "sa";
        public const string Password = "123456";
        public const string ExchangeName = "DataSenderExchange";
        public const string QueueNamePre = "DataSender";
        public const string RoutingKeyName = "DataSenderRoutingKey";
    }
}

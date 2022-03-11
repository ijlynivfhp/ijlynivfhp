using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.Common
{
    /// <summary>
    /// 基类
    /// </summary>
    public class RabbitMqFMTwoHelper
    {

        private static IConnection _connection;

        /// <summary>
        /// 服务器配置
        /// </summary>
        private static RabbitMqConfigModel RabbitConfig { get; set; }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public RabbitMqFMTwoHelper(RabbitMqConfigModel config)
        {
            try
            {
                RabbitConfig = config;
                CreateConn();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 创建连接
        /// </summary>
        private static void CreateConn()
        {
            ConnectionFactory cf = new ConnectionFactory();
            cf.Port = RabbitConfig.Port; //服务器的端口
            cf.Endpoint = new AmqpTcpEndpoint(new Uri("amqp://" + RabbitConfig.IP + "/")); //服务器ip
            cf.UserName = RabbitConfig.UserName; //登录账户
            cf.Password = RabbitConfig.Password; //登录账户
            //cf.VirtualHost = RabbitConfig.VirtualHost; //虚拟主机
            cf.RequestedHeartbeat = new TimeSpan(60); //虚拟主机

            _connection = cf.CreateConnection();
        }
        #endregion

        #region 发送消息
        /// <summary>
        /// 发送消息，泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Send<T>(T messageInfo, ref string errMsg)
        {
            if (messageInfo == null)
            {
                errMsg = "消息对象不能为空";
                return false;
            }
            string value = JsonConvert.SerializeObject(messageInfo);
            return Send(value, ref errMsg);
        }
        /// <summary>
        /// 发送消息，string类型
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool Send(string message, ref string errMsg)
        {
            if (string.IsNullOrEmpty(message))
            {
                errMsg = "消息不能为空";
                return false;
            }
            try
            {
                if (_connection is null||!_connection.IsOpen)
                {
                    CreateConn();
                }
                using (var channel = _connection.CreateModel())
                {
                    //推送消息
                    byte[] bytes = Encoding.UTF8.GetBytes(message);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = Convert.ToByte(RabbitConfig.DurableMessage ? 2 : 1); //支持可持久化数据

                    if (string.IsNullOrEmpty(RabbitConfig.ExchangeName))
                    {
                        //使用自定义的路由
                        channel.ExchangeDeclare(RabbitConfig.ExchangeName, RabbitConfig.ExchangeType.ToString(), RabbitConfig.DurableMessage, false, null);
                        channel.BasicPublish(message, RabbitConfig.QueueName, properties, bytes);
                    }
                    else
                    {
                        //申明消息队列，且为可持久化的，如果队列的名称不存在，系统会自动创建，有的话不会覆盖
                        channel.QueueDeclare(RabbitConfig.QueueName, RabbitConfig.DurableQueue, false, false, null);
                        channel.ExchangeDeclare(RabbitConfig.ExchangeName,RabbitConfig.ExchangeType.ToString(), RabbitConfig.DurableMessage, false, null);
                        channel.QueueBind(RabbitConfig.QueueName, RabbitConfig.ExchangeName, RabbitConfig.RoutingKey);
                        channel.BasicPublish(RabbitConfig.ExchangeName, RabbitConfig.RoutingKey, properties, bytes);
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 接受消息
        /// <summary>
        /// 接受消息，使用Action进行处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        public static void Receive<T>(Func<string, bool> method)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory
                {
                    //HostName = Constants.MqHost,
                    //Port = Constants.MqPort,
                    //UserName = Constants.MqUserName,
                    //Password = Constants.MqPwd
                };
                using (IConnection conn = factory.CreateConnection())
                using (IModel channel = conn.CreateModel())
                {

                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    channel.QueueDeclare("MyFirstQueue", true, false, false, null);
                    //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                    channel.BasicQos(0, 1, false);
                    Console.WriteLine("Listening...");

                    //该代码为原始程序
                    //在队列上定义一个消费者
                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var body = e.Body;
                        string message = Encoding.UTF8.GetString(body.ToArray());
                        //RequestMsg msg = JsonConvert.DeserializeObject<RequestMsg>(message);
                        Console.WriteLine("HandleMsg:" + message);
                        //Thread.Sleep(1000);
                        //回复确认
                        channel.BasicAck(e.DeliveryTag, false);
                    };
                    //进行消费
                    channel.BasicConsume("MyFirstQueue", false, consumer);
                    Console.WriteLine("Press Enter to Exit");
                    Console.ReadLine();
                    /*该代码为原始程序，正常。但QueueingBasicConsumer已经过时
                    //在队列上定义一个消费者
                    QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                    //消费队列，并设置应答模式为程序主动应答
                    channel.BasicConsume("MyFirstQueue", false, consumer);
                    while (true)
                    {
                        //阻塞函数，获取队列中的消息
                        BasicDeliverEventArgs ea = consumer.Queue.Dequeue();
                        byte[] bytes = ea.Body;
                        string str = Encoding.UTF8.GetString(bytes);
                        RequestMsg msg = JsonConvert.DeserializeObject<RequestMsg>(str);
                        Console.WriteLine("HandleMsg:" + msg.ToString());
                        //回复确认
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

    }
    public class RabbitFMOneHelper
    {
        string exchangeName = "demoexchange";
        string queueName = "demoqueue";
        string exchangeType = ExchangeType.Direct;
        string routingKey = "demoqueue";

        string userName = "test";
        string password = "test";
        string hostName = "127.0.0.1";
        int port = 5672;
        string virtualHost = "vhost";

        public delegate void MQMsgDelegate(string msg);
        public event MQMsgDelegate MQMsg;

        public delegate void MQErrorDeletegate(string error);
        public event MQErrorDeletegate MQError;

        /// <summary>
        /// 发布消息队列
        /// </summary>
        private Queue<string> ProducerQueue = new Queue<string>();

        private object obj = new object();
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string msg)
        {
            lock (obj)
            {
                ProducerQueue.Enqueue(msg);
            }
        }

        /// <summary>
        /// RabbitMQ
        /// </summary>
        /// <param name="exchangeName">消息交换机</param>
        /// <param name="queueName">消息队列</param>
        /// <param name="exchangeType">交换器类型</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="hostName">IP地址</param>
        /// <param name="port">端口</param>
        /// <param name="virtualHost">虚拟主机</param>
        public RabbitFMOneHelper(string exchangeName, string queueName, string exchangeType, string routingKey, string userName, string password, string hostName, int port, string virtualHost)
        {
            this.exchangeName = exchangeName;
            this.queueName = queueName;
            this.exchangeType = exchangeType;
            this.routingKey = routingKey;
            this.userName = userName;
            this.password = password;
            this.hostName = hostName;
            this.port = port;
            this.virtualHost = virtualHost;
        }

        /// <summary>
        /// 开始消费
        /// </summary>
        public void Consumer()
        {
            try
            {

                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = userName;
                factory.Password = password;
                factory.HostName = hostName;
                factory.Port = port;
                factory.VirtualHost = virtualHost;

                //factory.AutomaticRecoveryEnabled = true;
                using (var connection = factory.CreateConnection())
                {

                    using (var channel = connection.CreateModel())
                    {
                        //设置交换器的类型
                        channel.ExchangeDeclare(exchangeName, exchangeType);

                        //声明一个队列，设置队列是否持久化，排他性，与自动删除
                        channel.QueueDeclare(queueName, false, false, false, null);

                        //绑定消息队列，交换器，routingkey
                        channel.QueueBind(queueName, exchangeName, routingKey, null);

                        //流量控制
                        channel.BasicQos(0, 2, false);

                        while (true)
                        {
                            //消费数据
                            var consumer = new EventingBasicConsumer(channel);

                            //false为手动应答，true为自动应答
                            channel.BasicConsume(queueName, false, consumer);

                            consumer.Received += (ch, ea) =>
                            {
                                var body = ea.Body.ToArray();

                                MQMsg(Encoding.UTF8.GetString(body));

                                //Console.WriteLine("已接收： {0}", Encoding.UTF8.GetString(body));

                                //手动应答时使用
                                channel.BasicAck(ea.DeliveryTag, false);
                            };

                            string consumerTag = channel.BasicConsume(queueName, false, consumer);
                            channel.BasicCancel(consumerTag);

                            Thread.Sleep(1);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MQError(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 开始发布
        /// </summary>
        public void Producer()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = userName;
                factory.Password = password;
                factory.HostName = hostName;
                factory.Port = port;
                factory.VirtualHost = virtualHost;

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //设置交换器的类型
                        channel.ExchangeDeclare(exchangeName, exchangeType);

                        //声明一个队列，设置队列是否持久化，排他性，与自动删除
                        channel.QueueDeclare(queueName, false, false, false, null);

                        //绑定消息队列，交换器，routingkey
                        channel.QueueBind(queueName, exchangeName, routingKey, null);

                        //消息特点
                        var properties = channel.CreateBasicProperties();
                        properties.ContentType = "text/plain";
                        properties.DeliveryMode = 2;

                        while (true)
                        {
                            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                            watch.Start();//开始计时

                            Console.WriteLine("队列内数据量:" + (ProducerQueue.Count));//输出时间 毫秒
                            lock (obj)
                            {
                                if (ProducerQueue.Count > 0)
                                {
                                    while (ProducerQueue.Count > 0)
                                    {
                                        var sendMsg = ProducerQueue.Dequeue();

                                        //发送消息
                                        byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(sendMsg);
                                        channel.BasicPublish(exchangeName, routingKey, properties, messageBodyBytes);
                                        //Console.WriteLine("写入数据：" + sendMsg);

                                        //MQMsg(sendMsg +"待写入："+ ProducerQueue.Count);

                                        Thread.Sleep(1);
                                    }
                                }
                            }
                            watch.Stop();//停止计时

                            Console.WriteLine("耗时:" + (watch.ElapsedMilliseconds));//输出时间 毫秒


                            Thread.Sleep(1);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MQError(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}

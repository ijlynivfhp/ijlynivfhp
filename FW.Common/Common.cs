using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common
{
    
    #region 常量
    public class CommonString
    {
        /// <summary>
        /// 数据同步消息队列前缀
        /// </summary>
        public const string DataSender = "DataSender";
    }
    #endregion
    #region 枚举
    /// <summary>
    /// 枚举
    /// </summary>
    public enum CommonEnum
    {

    }

    public enum ExchangeTypeEnum
    {
        /// <summary>
        /// 不处理路由键。你只需要简单的将队列绑定到交换机上。一个发送到交换机的消息都会被转发到与该交换机绑定的所有队列上。
        /// 很像子网广播，每台子网内的主机都获得了一份复制的消息。Fanout交换机转发消息是最快的。
        /// </summary>
        fanout = 1,

        /// <summary>
        /// 处理路由键。需要将一个队列绑定到交换机上，要求该消息与一个特定的路由键完全匹配
        /// 。这是一个完整的匹配。如果一个队列绑定到该交换机上要求路由键 “dog”，
        /// 则只有被标记为“dog”的消息才被转发，不会转发dog.puppy，也不会转发dog.guard，只会转发dog。
        /// </summary>
        direct = 2,

        /// <summary>
        /// 将路由键和某模式进行匹配。此时队列需要绑定要一个模式上。
        /// 符号“#”匹配一个或多个词，符号“*”匹配不多不少一个词。
        /// 因此“audit.#”能够匹配到“audit.irs.corporate”，但是“audit.*” 只会匹配到“audit.irs”
        /// </summary>
        topic = 3,

        header = 4
    }

    /// <summary>
    /// 数据被执行后的处理方式
    /// </summary>
    public enum ProcessingResultsEnum
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        Accept,

        /// <summary>
        /// 可以重试的错误
        /// </summary>
        Retry,

        /// <summary>
        /// 无需重试的错误
        /// </summary>
        Reject,
    }

    /// <summary>
    /// 消息队列的配置信息
    /// </summary>
    public class RabbitMqConfigModel
    {
        #region host
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string IP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 服务器端口，默认是 5672
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; } = "sa";

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; } = "123456";
        /// <summary>
        /// 虚拟主机名称
        /// </summary>
        public string VirtualHost { get; set; } = "VirtualHostA";
        #endregion

        #region Queue
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; } = "QueueNameA";

        /// <summary>
        /// 是否持久化该队列
        /// </summary>
        public bool DurableQueue { get; set; } = default;
        #endregion

        #region exchange
        /// <summary>
        /// 路由名称
        /// </summary>
        public string ExchangeName { get; set; } = "ExchangeNameA";

        /// <summary>
        /// 路由的类型枚举
        /// </summary>
        public ExchangeTypeEnum ExchangeType { get; set; } = ExchangeTypeEnum.topic;

        /// <summary>
        /// 路由的关键字
        /// </summary>
        public string RoutingKey { get; set; } = "RoutingKeyA";

        #endregion

        #region message
        /// <summary>
        /// 是否持久化队列中的消息
        /// </summary>
        public bool DurableMessage { get; set; } = true;
        #endregion
    }

    #endregion

    #region RabbitMq
    public class ExchangeType
    {
        public const string Direct = "Direct";
        public const string Topic = "Topic";
        public const string Fanout = "Fanout";
        public const string Headers = "Headers";
    }

    public class RabbitMQCommon {
        public const string RABBITMQ_DEFAULT_VIRTUAL_HOST = "RABBITMQ_DEFAULT_VIRTUAL_HOST";
        public const int RABBITMQ_PORT = default;
        public const string RABBITMQ_HOST = "RABBITMQ_HOST";
    }
    #endregion
}

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Com.Ocamar.DataEngine.BAL.Helper;
using Com.OCAMAR.Common.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Cache.ActiveMQ
{
    /// <summary>
    /// MQ 帮助类
    /// </summary>
    public static class ActiveMQHelper
    {
        private static string _mqhost
                    = ConfigurationManager.AppSettings["MQHost"]; // MQ 服务器

        private static IConnectionFactory _factory = new ConnectionFactory($"tcp://{_mqhost}");

        /// <summary>
        /// 插入MQ
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static void InsertMQ(byte[] buffer)
        {
            using (IConnection connection = _factory.CreateConnection())
            {
                //Create the Session  
                using (ISession session = connection.CreateSession())
                {
                    //Create the Producer for the topic/queue  
                    IMessageProducer prod = session.CreateProducer(
                        new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(StatusBug.MQTipic));

                    var msg = prod.CreateBytesMessage();
                    msg.Content = buffer;
                    LogWriter.Info("SendingMQ: " + BitConverter.ToString(buffer));
                    prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent,
                        Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
                }
            }
        }
    }
}

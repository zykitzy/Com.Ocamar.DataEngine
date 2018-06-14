using Apache.NMS;
using Com.Ocamar.DataEngine.BAL.Helper;
using Com.OCAMAR.Common.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Service.Watcher
{
    public class ReceiveHelper
    {
        private IConnectionFactory _factory;
        public ReceiveHelper(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public void ReceiveData(object newclient)
        {

            TcpClient client = newclient as TcpClient; // 一个接入的对象
            if (client == null)
            {
                return;
            }
            while (true)
            {
                if (!client.Connected || (client.Client.Poll(20, SelectMode.SelectRead) 
                    && client.Available == 0))
                {
                    LogWriter.Info($"来自 {client.Client.RemoteEndPoint} 的连接断开");
                    client.Close();
                    return;
                }
                //在接收数据时，如果可读字节流是 0 ，则跳过
                if (client.Available < 1)
                {
                    continue;
                }
                //因为有包粘连，所以一次尽可能读取长的字节
                //而非读取一部分，其它留在缓冲区
                byte[] buffer = new byte[client.Available];
                try
                {
                    var read = new BinaryReader(client.GetStream());
                    buffer = read.ReadBytes(buffer.Length);

                    InsertMQ(buffer); //插入MQ
                }
                catch (SocketException ex)
                {
                    // 当对方主动断开链接时，ReadBytes 会报错，此时断开链接
                    LogWriter.Info($"来自 {client.Client.RemoteEndPoint} 的连接断开");
                    LogWriter.Error(ex.ToString());
                    client.Close();
                    return;
                }
                catch (Exception ex)
                {
                    LogWriter.Error("发生未知错误 ： ");
                    LogWriter.Error(ex.ToString());
                    client.Close();
                    return;
                }

            }
        }

        /// <summary>
        /// 插入MQ
        /// </summary>
        /// <param name="buffer">字节流</param>
        private void InsertMQ(byte[] buffer)
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

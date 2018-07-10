using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Com.OCAMAR.Common.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Com.Ocamar.DataEngine.Service.Watcher
{
    /// <summary>
    /// 监听服务：
    /// 1：开启线程，监听端口
    /// 2：将数据存入消息队列
    /// 3：如果是上线请求，需要立即响应
    /// todo: 4：新来的基站请求进行校验 
    /// todo: 5：校验新上线终端 Device ID 是否可以使用，不合法的，需要重新分配
    /// </summary>
    public class WatchService
    {
        private readonly string _port
            = ConfigurationManager.AppSettings["ListenPort"]; // 监听端口


        private TcpListener _watcher; // 监听者，需要单独线程开启监听所有访问本机IP的
        private IConnectionFactory _factory; // 消息队列
       
        /// <summary>
        /// 初始化读取配置信息
        /// </summary>
        public WatchService()
        {
            IPAddress local = IPAddress.Any;
            _watcher = new TcpListener(local, Convert.ToInt32(_port));
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        public void StartListen()
        {
            Thread _listen = new Thread(Listen);
            _listen.Start();
            LogWriter.Info($"开始监听: {_port} 端口");
        }

        /// <summary>
        /// 监听后的数据处理方法
        /// </summary>
        /// <param name="listen"></param>
        private void Listen()
        {
            try
            {
                //开始监听
                _watcher.Start();
                var receive = new ReceiveHelper();

                while (true)
                {
                    var client = _watcher.AcceptTcpClient();
                    LogWriter.Info($"来自{client.Client.RemoteEndPoint}的链接");
                    receive.ReceiveData(client); // 开始接受数据
                }

            }
            catch (Exception ex)
            {
                LogWriter.Error("监听发生错误：");
                LogWriter.Error(ex.ToString());
                _watcher.Stop();// 先停止监听

                // 重新唤醒监听并且继续
                StartListen();
            }
        }
    }
}

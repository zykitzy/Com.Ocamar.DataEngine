using Com.Ocamar.DataEngine.Common.Helper.Status;
using Com.Ocamar.DataEngine.Service.Formatter;
using Com.OCAMAR.Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Service.Watcher.Response
{
    /// <summary>
    /// 同步终端请求
    /// </summary>
    public class ClientSync
    {
        /// <summary>
        /// 根据消息的类型，响应客户端
        /// </summary>
        /// <param name="data">终端上传的数据</param>
        private bool ResponseData(byte[] buffer, NetworkStream stream)
        {
            var format = new ActionSyncFormat(buffer);
            if (format.IsValid())
            {
                var response = format.ClientOnlineData();

                LogWriter.Info($"组织响应数据 - :{BitConverter.ToString(response)}");

                stream.Write(response, 0, response.Length); //发送请求
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断字节中有没有上线请求
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public void ClientsOnline(byte[] buffer, NetworkStream stream)
        {
            // 可能一次流中有多个上线请求，一起响应
            try
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    // 判断开始符
                    if (buffer[i] == GlobalStatus.SOF)
                    {

                        if (buffer[i + GlobalStatus.DataLengthOffset]
                            == Convert.ToByte(GlobalStatus.ClientOnline.RequestLen - 2)// 判断Length
                            && buffer[i + GlobalStatus.DataCommandOffset]
                            == GlobalStatus.ClientOnline.RequestType //Command Type
                            && buffer[GlobalStatus.ClientOnline.RequestLen - 1] == GlobalStatus.EOF // 终止符
                            )
                        {
                            byte[] client = new byte[GlobalStatus.ClientOnline.RequestLen];
                            Buffer.BlockCopy(buffer, i, client, 0, GlobalStatus.ClientOnline.RequestLen);

                            LogWriter.Info($"监测上线请求 - :{BitConverter.ToString(client)}");

                            if (ResponseData(client, stream))
                            {
                                LogWriter.Info($"上线成功！");
                            }
                            else
                            {
                                LogWriter.Info($"上线失败！");
                            }
                        }
                    }
                }
            }
            catch (SocketException sex)
            {
                LogWriter.Error($"响应同步出错：{sex}");
                if (stream.CanWrite)
                {
                    stream.Close();//关闭流
                }
                throw sex;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Common.Helper.Status
{
    /// <summary>
    /// 所有静态约定参数
    /// </summary>
    public static class GlobalStatus
    {
        /// <summary>
        /// 在MQ中存储的消息广播队列标志
        /// </summary>
        public const string MQTopic = "Ocamar";

        /// <summary>
        /// 消息队列起始字节
        /// </summary>
        public const byte SOF = 0x3e;
        /// <summary>
        /// 消息队列起始字节
        /// </summary>
        public const byte EOF = 0x3f;

        /// <summary>
        ///  SOF 偏移量
        /// </summary>
        public const int DataSOFOffset = 0;
        /// <summary>
        ///  描述长度的 偏移量
        /// </summary>
        public const int DataLengthOffset = 1;
        /// <summary>
        ///  描述命令类型的 偏移量
        /// </summary>
        public const int DataCommandOffset = 2;

        /// <summary>
        /// 终端请求同步响应
        /// </summary>
        public static class ClientOnline
        {
            /// <summary>
            ///  请求 Action 同步数据(B2D) 数据长度
            /// </summary>
            public const int RequestLen = 17;
            /// <summary>
            ///  请求 Action 同步数据(D2B) 响应数据长度
            /// </summary>
            public const int ResponseLen = 15;
            /// <summary>
            ///  请求 Action 同步数据(B2D) 数据 Command Type
            /// </summary>
            public const byte RequestType = DataFlagStatus.DataFlagByte.ClientOnline;

            /// <summary>
            ///  请求 Action 同步数据(D2B) 数据响应 Command Type
            /// </summary>
            public const byte ResponseType = 0xA2;

        }
    }
}

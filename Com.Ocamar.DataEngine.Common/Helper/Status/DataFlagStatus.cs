using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Common.Helper.Status
{
    /// <summary>
    /// 硬件数据类型标志
    /// </summary>
    public static class DataFlagStatus
    {
        /// <summary>
        /// 终端心跳
        /// </summary>
        public const string ClientHeartBeat = "ClientHeartBeat";
        /// <summary>
        /// 基站心跳
        /// </summary>
        public const string StationHeartBeat = "StationHeartBeat";
        /// <summary>
        /// 终端上线请求
        /// </summary>
        public const string ClientOnline = "ClientOnline";
        /// <summary>
        /// 终端数据请求
        /// </summary>
        public const string ClientDataReport = "ClientDataReport";
        /// <summary>
        /// 终端事件
        /// </summary>
        public const string ClientEvent = "ClientEvent";
        /// <summary>
        /// 终端参数返回
        /// </summary>
        public const string ClientParaReturn = "ClientParaReturn";
        /// <summary>
        /// 终端参数设置状态返回
        /// </summary>
        public const string ClientParaStatus = "ClientParaStatus";
        /// <summary>
        /// 终端用药数据返回
        /// </summary>
        public const string ClientMedication = "ClientMedication";

        /// <summary>
        /// 根据值的对照表，参数名称必须和上一级（DataFlag）相同
        /// </summary>
        public static class DataFlagByte
        {
            /// <summary>
            /// 终端心跳
            /// </summary>
            public const byte ClientHeartBeat = 0x24;
            /// <summary>
            /// 基站心跳
            /// </summary>
            public const byte StationHeartBeat = 0xE0;
            /// <summary>
            /// 终端上线请求
            /// </summary>
            public const byte ClientOnline = 0x22;
            /// <summary>
            /// 终端数据请求
            /// </summary>
            public const byte ClientDataReport = 0x27;
            /// <summary>
            /// 终端事件
            /// </summary>
            public const byte ClientEvent = 0x25;
            /// <summary>
            /// 终端参数返回
            /// </summary>
            public const byte ClientParaReturn = 0x28;
            /// <summary>
            /// 终端参数设置状态返回
            /// </summary>
            public const byte ClientParaStatus = 0x29;
            /// <summary>
            /// 终端用药数据返回
            /// </summary>
            public const byte ClientMedication = 0x2A;
        }
    }
}

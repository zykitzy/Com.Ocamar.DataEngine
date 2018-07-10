using Com.Ocamar.DataEngine.Common.Helper.DataValid;
using Com.Ocamar.DataEngine.Common.Helper.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Service.Formatter
{
    /// <summary>
    /// 同步响应格式化辅助
    /// </summary>
    public class ActionSyncFormat : FormatBase
    {
        public ActionSyncFormat(byte[] data) : base(data) { }

        /// <summary>
        /// 响应上线请求的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] ClientOnlineData(byte[] buffer)
        {
            byte[] response = new byte[GlobalStatus.ClientOnline.ResponseLen];
            byte reslen = Convert.ToByte(GlobalStatus.ClientOnline.ResponseLen - 2);


            response[GlobalStatus.DataSOFOffset] = GlobalStatus.SOF;
            response[GlobalStatus.DataSOFOffset + GlobalStatus.DataLengthOffset] = reslen;
            response[GlobalStatus.DataSOFOffset + GlobalStatus.DataCommandOffset]
                = GlobalStatus.ClientOnline.ResponseType; //响应标准
            response[3] = buffer[3];
            response[4] = buffer[4];
            response[5] = buffer[5];
            response[6] = buffer[6];
            response[7] = buffer[7];
            response[8] = buffer[8];
            response[9] = buffer[9];
            response[10] = buffer[10];
            response[11] = buffer[11];
            response[12] = buffer[12];
            response[13] = buffer[13];
            response[14] = GlobalStatus.EOF;
            // 处理CRC
            byte[] crc = new byte[response.Length - 4];
            Buffer.BlockCopy(response, 1, crc, 0, response.Length - 4);
            var crc_convert = CRCHelper.CCITT_CRC16_H2L(crc);
            response[12] = crc_convert[0];
            response[13] = crc_convert[1];

            return response;

        }

        /// <summary>
        /// 响应上线请求的数据
        /// </summary>
        /// <returns></returns>
        public byte[] ClientOnlineData()
        {
            return ClientOnlineData(Data);
        }

        /// <summary>
        /// 数据合法性检查
        /// </summary>
        /// <returns>是否合法</returns>
        public override bool IsValid()
        {
            // 长度不匹配
            if (Data.Length != GlobalStatus.ClientOnline.RequestLen)
            {
                return false;
            }
            // 开始或者结束符不匹配
            if (Data[GlobalStatus.DataSOFOffset] != GlobalStatus.SOF
                || Data[GlobalStatus.ClientOnline.RequestLen - 1] != GlobalStatus.EOF)
            {
                return false;
            }
            // 标志位不匹配
            if (Data[GlobalStatus.DataSOFOffset + GlobalStatus.DataCommandOffset]
                != GlobalStatus.ClientOnline.RequestType)
            {
                return false;
            }
            return CRCHelper.CheckCRC(Data); // CRC 验证
        }
    }
}

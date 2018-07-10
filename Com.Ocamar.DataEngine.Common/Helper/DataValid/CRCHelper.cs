using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Common.Helper.DataValid
{
    public enum InitialCrcValue { Zeros, NonZero1 = 0xffff, NonZero2 = 0x1D0F }
    public static class CRCHelper
    {
        /// <summary>
        /// CRC16 校验
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        [Obsolete]
        public static int GetCrc16(byte[] bytes)
        {
            ushort crc = 0x0000;
            ushort current;

            for (int i = 0; i < bytes.Length; i++)
            {
                current = bytes[i];
                for (int j = 0; j < 8; j++)
                {
                    if (((crc ^ current) & 0x0001) != 0)
                    {
                        crc = (ushort)((crc >> 1) ^ 0x8408);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                    current >>= 1;
                }
            }
            return crc;
        }

        /// <summary>
        /// CCITT标准CRC 16 校验
        /// </summary>
        /// <param name="bytes">需要校验的数据</param>
        /// <returns></returns>
        public static byte[] CCITT_CRC16(byte[] bytes)
        {
            ushort crc = 0x0000;
            for (int j = 0; j < bytes.Length; j++)
            {
                var current = bytes[j];
                crc = (ushort)(crc ^ current);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                        crc = (ushort)((crc >> 1) ^ 0x8408);
                    else
                        crc >>= 1;
                }
                current >>= 1;
            }
            return BitConverter.GetBytes(crc);
        }
        /// <summary>
        /// CCITT标准CRC 16 校验,高位倒低位
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] CCITT_CRC16_H2L(byte[] bytes)
        {
            var crcbyte = CCITT_CRC16(bytes);
            var convert = new byte[2];
            convert[0] = crcbyte[1];
            convert[1] = crcbyte[0];
            return convert;
        }

        /// <summary>
        /// 校验数据是否合法
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static bool CheckCRC(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            byte SOF = buffer[0];
            //length 是出去SOF 和EOF 的总长度
            int length = Convert.ToInt32(buffer[1]);
            byte EOF = buffer[length + 1];
            //判断结束位
            if (EOF != 0x3F)
            {
                return false;
            }
            byte[] sourceCRC = new byte[2];
            //获取CRC 字节
            Buffer.BlockCopy(buffer, length - 1, sourceCRC, 0, 2);
            //判断CRC ,长度为出去 BOF 和 EOF ，CRC
            byte[] CRCbyte = new byte[length - 2];
            Buffer.BlockCopy(buffer, 1, CRCbyte, 0, length - 2);

            //转出的CRC 始终是低位到高位，于C代码中高位到低位相反
            var convertCRC = CCITT_CRC16(CRCbyte);
            if (convertCRC[1] == sourceCRC[0] && convertCRC[0] == sourceCRC[1])
            {
                return true;
            }
            return false;
        }
    }
}

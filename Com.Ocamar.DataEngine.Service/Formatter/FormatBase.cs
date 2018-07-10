using Com.Ocamar.DataEngine.Common.Helper.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.Service.Formatter
{
    public abstract class FormatBase
    {
        public byte[] Data { get; set; }
        public FormatBase()
        {
        }
        public FormatBase(byte[] data)
        {
            Data = data;
        }
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <returns></returns>
        public string DataFlag
        {
            get
            {


                if (Data.Length < 3)
                {
                    throw new ArgumentNullException(nameof(Data), "参数错误，不能判断类型");
                }
                byte flag = Data[2]; //第三位为 command type
                #region 反射速度太慢，此处修改为判断
                //var flagbyteinfos = typeof(DataFlag.DataFlagByte).GetFields();
                //foreach (var flagbyte in flagbyteinfos)
                //{
                //    var byteval = Convert.ToByte(flagbyte.GetValue(null));
                //    if (byteval == flag)
                //    {
                //        //两层反射获取值，防止字典项改变
                //        //为获取效率，也可以用 Switch Case
                //       return typeof(DataFlag).GetField(flagbyte.Name).GetValue(null).ToString();
                //    }
                //}
                #endregion
                switch (flag)
                {
                    case DataFlagStatus.DataFlagByte.ClientDataReport:
                        return DataFlagStatus.ClientDataReport;
                    case DataFlagStatus.DataFlagByte.ClientEvent:
                        return DataFlagStatus.ClientEvent;
                    case DataFlagStatus.DataFlagByte.ClientHeartBeat:
                        return DataFlagStatus.ClientHeartBeat;
                    case DataFlagStatus.DataFlagByte.ClientMedication:
                        return DataFlagStatus.ClientMedication;
                    case DataFlagStatus.DataFlagByte.ClientOnline:
                        return DataFlagStatus.ClientOnline;
                    case DataFlagStatus.DataFlagByte.ClientParaReturn:
                        return DataFlagStatus.ClientParaReturn;
                    case DataFlagStatus.DataFlagByte.ClientParaStatus:
                        return DataFlagStatus.ClientParaStatus;
                    case DataFlagStatus.DataFlagByte.StationHeartBeat:
                        return DataFlagStatus.StationHeartBeat;
                }

                return "";
            }
        }



        /// <summary>
        /// 判断数据是否合法
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();

    }
}

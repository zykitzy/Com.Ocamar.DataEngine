using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.DAL.Entities.Model
{
    public class StationManager
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 基站编号
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 是否准入
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
    }
}

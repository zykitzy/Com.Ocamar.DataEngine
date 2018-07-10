using Com.Ocamar.DataEngine.Cache.Redis;
using Com.Ocamar.DataEngine.DAL.Api;
using Com.Ocamar.DataEngine.DAL.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.BAL.Apis.SysConfig
{
    public class StationManagerBLL : BaseBLL
    {
        private StationManagerDAL dal;

        private readonly string rediskey = "StationManager";
        /// <summary>
        /// 构造函数
        /// </summary>
        public StationManagerBLL()
        {
            dal = new StationManagerDAL();
        }
        /// <summary>
        /// 将数据初始化到缓存
        /// </summary>
        public void InitToCache()
        {
            var list = dal.GetAll();
            var redis = new RedisHelper();
            redis.ListRightPush<IEnumerable<StationManager>>(rediskey, list);
        }

     
    }
}

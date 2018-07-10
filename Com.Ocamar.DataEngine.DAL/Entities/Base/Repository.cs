using Com.OCAMAR.Common.Library.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.DAL.Entities.Base
{
    /// <summary>
    /// 数据存储基类
    /// </summary>
    /// <typeparam name="Context">上下文对象类型</typeparam>
    /// <typeparam name="T">实体类型</typeparam>
    public class Repository<Context, T> : BaseRepository<Context, T>
        where Context : DbContext
        where T : class
    {

    }
}

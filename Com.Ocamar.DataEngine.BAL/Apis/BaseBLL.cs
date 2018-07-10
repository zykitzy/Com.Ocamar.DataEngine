using EmitMapper;
using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ocamar.DataEngine.BAL.Apis
{
    public class BaseBLL
    {

        /// <summary>
        /// 模型转换
        /// </summary>
        /// <typeparam name="TFrom">from 模型</typeparam>
        /// <typeparam name="TTo">To 模型</typeparam>
        /// <param name="model">from 模型实体</param>
        /// <returns></returns>
        protected TTo Mapping<TFrom, TTo>(TFrom model)
        {

            // var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(config);
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>();
            var mapmodel = mapper.Map(model);
            return mapmodel;
        }



        /// <summary>
        /// 模型转换
        /// </summary>
        /// <typeparam name="TFrom">来源模型</typeparam>
        /// <typeparam name="TTo">目标模型</typeparam>
        /// <param name="model">源对象</param>
        /// <param name="tmodel">目标对象</param>
        /// <param name="propertySelector">指定属性赋值设置</param>
        /// <returns>目标对象</returns>
        protected TTo Mapping<TFrom, TTo>(TFrom model, TTo tmodel, Func<string, string, bool> propertySelector = null)
        {
            var config = new DefaultMapConfig();
            if (propertySelector != null)
                config.MatchMembers(propertySelector);

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(config);
            mapper.Map(model, tmodel);
            return tmodel;
        }

        /// <summary>
        /// 模型转换
        /// </summary>
        /// <typeparam name="TFrom">来源模型</typeparam>
        /// <typeparam name="TTo">目标模型</typeparam>
        /// <param name="model">源对象</param>
        /// <param name="tmodel">目标对象</param>
        /// <param name="ignoreMembers">被忽略的不被赋值的属性</param>
        /// <returns>目标对象</returns>
        protected TTo MappingIgnore<TFrom, TTo>(TFrom model, TTo tmodel, params string[] ignoreMembers)
        {
            var config = new DefaultMapConfig();
            if (ignoreMembers != null)
                config.IgnoreMembers<TFrom, TTo>(ignoreMembers);

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(config);
            mapper.Map(model, tmodel);
            return tmodel;
        }
        /// <summary>
        /// 模型转换
        /// </summary>
        /// <typeparam name="TFrom">来源模型</typeparam>
        /// <typeparam name="TTo">目标模型</typeparam>
        /// <param name="model">源对象</param>
        /// <param name="tmodel">目标对象</param>
        /// <returns>目标对象</returns>
        protected TTo MappingIgnoreDefault<TFrom, TTo>(TFrom model, TTo tmodel)
        {
            //var config = new DefaultMapConfig();
            //if (ignoreMembers != null)
            //    config.IgnoreMembers<TFrom, TTo>(ignoreMembers);

            var config = new DefaultMapConfig();
            // 不更新ID 和 Data_CreateTime
            var ignoreMembers = new string[] { "ID", "Data_CreateTime", "CreateUser" };
            config.IgnoreMembers<TFrom, TTo>(ignoreMembers);

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(config);
            mapper.Map(model, tmodel);
            return tmodel;
        }

    }
}

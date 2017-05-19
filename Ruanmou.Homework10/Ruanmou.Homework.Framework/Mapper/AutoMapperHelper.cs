using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ruanmou.Homework.Framework.Mapper
{
    /// <summary>
    ///  auto mapper辅助类
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        ///  对象映射
        /// </summary>
        /// <typeparam name="T">要转换的目标对象</typeparam>
        /// <param name="entity">被转换的源对象</param>
        /// <returns></returns>
        public static T Mapper<T>(this object entity)
        {
            return AutoMapper.Mapper.DynamicMap<T>(entity);
        }
    }
}

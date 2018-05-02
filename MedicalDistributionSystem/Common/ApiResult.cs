using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalDistributionSystem.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 响应编码
        /// </summary>
        public int Code { get; set; } = 200;
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; } = default(T);
        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool IsSuccess { get; set; } = true;
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Msg { get; set; } = "SUCCESSS";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 响应编码
        /// </summary>
        public int Code { get; set; } = 200;
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Msg { get; set; } = "请求成功";
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; } = 0;
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; } = default(T);
        /// <summary>
        /// 指示是否处理成功
        /// </summary>
        public bool IsSuccess { get; set; } = true;
    }
}
//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/27 14:17:59
//  文件名：ApiResult
//  版本：V1.0.0 
//  说明：  
//==============================================================
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
    public class ApiResult<T>
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
}
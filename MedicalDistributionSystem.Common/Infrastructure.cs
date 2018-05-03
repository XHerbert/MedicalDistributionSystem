//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/27 14:17:59
//  文件名：Infrastructure
//  版本：V1.0.0 
//  说明：  
//==============================================================
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MedicalDistributionSystem.Common
{
    /// <summary>
    /// 公共类库
    /// </summary>
    public class Infrastructure
    {
        /// <summary>
        /// 产生Token
        /// </summary>
        /// <returns></returns>
        public static string CreateToken()
        {
            return Guid.NewGuid().ToString().Trim().Replace("-","").ToUpper();
        }

        /// <summary>
        /// 获取指定的配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// 获取当前是否为生产环境
        /// </summary>
        /// <returns></returns>
        public static bool IsTestEnvironment()
        {
            return GetConfig("TestFlag").Equals("True");
        }

        /// <summary>
        /// 获取代理级别
        /// </summary>
        /// <param name="ProxyLevel"></param>
        /// <returns></returns>
        public static string GetProxyLevel(int ProxyLevel)
        {
            string proxyLevel = string.Empty;
            switch (ProxyLevel)
            {

                case 1:
                    proxyLevel = "超级管理员";
                    break;
                case 2:
                    proxyLevel = "一级代理";
                    break;
                case 3:
                    proxyLevel = "二级代理";
                    break;
                case 4:
                    proxyLevel = "三级代理";
                    break;
                default:
                    break;
            }
            return proxyLevel;
        }
    }
}
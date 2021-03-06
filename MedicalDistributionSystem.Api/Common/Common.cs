﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MedicalDistributionSystem.Api.Common
{
    /// <summary>
    /// 公共类库
    /// </summary>
    public class Common
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
    }
}
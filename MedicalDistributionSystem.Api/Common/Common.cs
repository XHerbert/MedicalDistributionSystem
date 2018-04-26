using System;
using System.Collections.Generic;
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
    }
}
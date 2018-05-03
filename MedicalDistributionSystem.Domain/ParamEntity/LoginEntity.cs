//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/3 10:37:08
//  文件名：LoginEntity
//  版本：V1.0.0 
//  说明：  
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.ParamEntity
{
    public class LoginEntity
    {
        public string account { get; set; }
        public string password { get; set; }
        public int type { get; set; } = 1;
    }
}

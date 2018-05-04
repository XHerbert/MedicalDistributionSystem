//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/4 10:52:30
//  文件名：OperatorModel
//  版本：V1.0.0 
//  说明：  
//==============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    public class OperatorModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string Role { get; set; }
        public string LoginIPAddress { get; set; }
        public string LoginIPAddressName { get; set; }
        public string LoginToken { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsAdmin { get; set; }
        public int Level { get; set; }
    }
}

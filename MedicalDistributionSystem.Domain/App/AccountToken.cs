//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/27 9:52:45
//  文件名：AccountToken
//  版本：V1.0.0 
//  说明：
//==============================================================
using System;

namespace MedicalDistributionSystem.Domain.App
{
    public class AccountToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Account { get; set; }
        public int AccountType { get; set; }
        public DateTime? CreatorTime { get; set; }
        public bool? DeleteMark { get; set; } = false;
    }
}

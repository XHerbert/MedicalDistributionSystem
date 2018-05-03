//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/3 11:38:55
//  文件名：Log
//  版本：V1.0.0 
//  说明：  
//==============================================================
using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    public class Log : IEntity<Log>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public int Id { get; set; }
        public string LogContent { get; set; }
        public int LogType { get; set; }
        public string LogUrl { get; set; }
        public DateTime? CreatorTime { get; set; }
        public int CreatorUserId { get; set; }
        public bool? DeleteMark { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int DeleteUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public int LastModifyUserId { get; set; }
    }
}

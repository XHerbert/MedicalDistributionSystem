//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/3 11:47:30
//  文件名：LogMap
//  版本：V1.0.0 
//  说明：  
//==============================================================
using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.Maps
{
    public class LogMap:EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("Log");
            this.HasKey(p => p.Id);
        }
    }
}

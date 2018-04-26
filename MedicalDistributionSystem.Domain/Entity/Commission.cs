using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    /// <summary>
    /// 佣金流水 (一条佣金流水记录只属于一个代理人)
    /// </summary>
    public class Commission : IEntity<Commission>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 流水Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 代理人Id
        /// </summary>
        public int ProxyId { get; set; }
        /// <summary>
        /// 佣金金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 佣金收入方式
        /// </summary>
        public int IncomeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CreatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LastModifyUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? DeleteMark { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public int DeleteUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

    }
}

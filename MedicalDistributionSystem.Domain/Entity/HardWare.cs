using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    /// <summary>
    /// 硬件
    /// </summary>
    public class HardWare : IEntity<HardWare>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 硬件Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 硬件代理人
        /// </summary>
        public int ProxyId { get; set; }
        /// <summary>
        /// 使用时长（秒）
        /// </summary>
        public int UsedSeconds { get; set; }
        /// <summary>
        /// 可用时长（秒）
        /// </summary>
        public int UseableSeconds { get; set; }
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
        public bool? DeleteMark { get; set; }
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

using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    /// <summary>
    /// 病历（消费记录）（一条病历只属于一个会员）
    /// </summary>
    public class MedicalRecord : IEntity<MedicalRecord>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 会员（病人）Id
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 就诊医生
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 就医时间
        /// </summary>
        public DateTime? MedicalTime { get; set; }
        /// <summary>
        /// 病情
        /// </summary>
        public string Illness { get; set; }
        /// <summary>
        /// 药方
        /// </summary>
        public string Prescription { get; set; }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal Fee { get; set; }
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

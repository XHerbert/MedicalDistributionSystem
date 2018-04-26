using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    /// <summary>
    /// 会员（病人）
    /// </summary>
    public class Member : IEntity<Member>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 会员Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 所属代理
        /// </summary>
        public int ProxyId { get; set; }
        /// <summary>
        /// 会员电话（账号）
        /// </summary>
        [MaxLength(11)]
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 会员性别
        /// </summary>
        public short Gender { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 会员病历(消费记录)
        /// </summary>
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
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

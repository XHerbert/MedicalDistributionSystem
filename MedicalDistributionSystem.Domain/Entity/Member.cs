using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
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
        public string mobile { get; set; }
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

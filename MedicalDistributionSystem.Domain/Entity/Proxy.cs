using MedicalDistributionSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Entity
{
    /// <summary>
    /// 代理人
    /// </summary>
    public class Proxy : IEntity<Proxy>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 代理Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ProxyName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProxyCode { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal CurrentMoney { get; set; }
        /// <summary>
        /// 佣金比例
        /// </summary>
        public decimal BackMoneyPercent { get; set; }
        /// <summary>
        /// 代理级别
        /// </summary>
        public int ProxyLevel { get; set; }
        /// <summary>
        /// 下属会员
        /// </summary>
        public List<Member> ProxyMembers { get; set; }
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

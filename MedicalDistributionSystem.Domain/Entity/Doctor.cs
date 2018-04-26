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
    /// 医生
    /// </summary>
    public class Doctor : IEntity<Doctor>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 医生Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(11)]
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
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

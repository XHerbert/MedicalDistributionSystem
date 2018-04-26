using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Enums
{
    public class Medical
    {
        /// <summary>
        /// 代理级别
        /// </summary>
        public enum ProxyLevelEnums
        {
            /// <summary>
            /// 一级代理
            /// </summary>
            LevelOne = 0,
            /// <summary>
            /// 二级代理
            /// </summary>
            LevelTwo = 1,
            /// <summary>
            /// 三级代理
            /// </summary>
            LevelThree = 2
        }

        /// <summary>
        /// 代理收入方式
        /// </summary>
        public enum IncomeType
        {
            /// <summary>
            /// 下属会员消费
            /// </summary>
            FromMember = 0,
            /// <summary>
            /// 下属代理抽成
            /// </summary>
            FromProxy = 1
        }

        /// <summary>
        /// 账户类型
        /// </summary>
        public enum AccountType
        {
            /// <summary>
            /// 会员（病人）
            /// </summary>
            MemberType = 0,
            /// <summary>
            /// 代理
            /// </summary>
            ProxyType = 1
        }
    }
}

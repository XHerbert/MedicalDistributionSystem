using MedicalDistributionSystem.Api.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using MedicalDistributionSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MedicalDistributionSystem.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : ApiController
    {
        /// <summary>
        /// 代理/会员登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ApiResult<object> Login(string account,string password,int type = 1)
        {
            ApiResult<object> result = new ApiResult<object>();
            using (var db = new MDDbContext())
            {
                if(type == (int)Medical.AccountType.ProxyType)
                {
                    var proxy = db.Proxies.Where(p => p.DeleteMark == false && p.Mobile == account.Trim() && p.Password == password).FirstOrDefault();
                    if (proxy != null)
                    {
                        result.Data = proxy;
                    }
                    else
                    {
                        result.Data = null;
                        result.Msg = Resource.ACCOUNT_ERROR;
                        result.Code = 500;
                        result.IsSuccess = false;
                    }
                }
                if(type == (int)Medical.AccountType.MemberType)
                {
                    var member = db.Members.Where(m => m.DeleteMark == false && m.Mobile == account.Trim() && m.Password == password).FirstOrDefault();
                    if (member != null)
                    {
                        result.Data = member;
                    }
                    else
                    {
                        result.Data = null;
                        result.Msg = Resource.ACCOUNT_ERROR;
                        result.Code = 500;
                        result.IsSuccess = false;
                    }
                }
            }
            return result;
        }
    }
}

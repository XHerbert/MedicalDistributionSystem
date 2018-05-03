//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/26 9:52:45
//  文件名：LoginController
//  版本：V1.0.0 
//  说明：
//==============================================================

using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.App;
using MedicalDistributionSystem.Domain.Enums;
using MedicalDistributionSystem.Domain.ParamEntity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace MedicalDistributionSystem.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        /// <summary>
        /// 代理/会员登录
        /// </summary>
        /// <param name="loginEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Login(LoginEntity loginEntity)
        {
            ApiResult<object> result = new ApiResult<object>();
            using (var db = new MDDbContext())
            {
                if(loginEntity.type == (int)Medical.AccountType.ProxyType)
                {
                    Expression<Func<Domain.Entity.Proxy, bool>> where = p => p.DeleteMark == false && ((p.Mobile == loginEntity.account.Trim()) || (p.ProxyName == loginEntity.account.Trim())) && MD5Helper.VerifyMd5Hash(loginEntity.password, p.Password);
                    var proxy = db.Proxies.Where(where.Compile()).FirstOrDefault();
                    if (proxy != null)
                    {
                        var token = new AccountToken();
                        token.Token = Common.Infrastructure.CreateToken();
                        token.AccountType = (int)Medical.AccountType.ProxyType;
                        token.Account = proxy.Mobile;
                        token.CreatorTime = DateTime.Now;
                        db.Entry<AccountToken>(token).State = System.Data.Entity.EntityState.Added;
                        db.AccountTokens.Add(token);
                        db.SaveChanges();
                        var data = new
                        {
                            proxy = proxy,
                            token = token.Token
                        };
                        result.Data = data;
                    }
                    else
                    {
                        result.Data = null;
                        result.Msg = Resource.ACCOUNT_ERROR;
                        result.Code = 500;
                        result.IsSuccess = false;
                    }
                }
                if(loginEntity.type == (int)Medical.AccountType.MemberType)
                {
                    var member = db.Members.Where(m => m.DeleteMark == false && (m.Mobile == loginEntity.account.Trim()||(m.MemberName== loginEntity.account.Trim())) && m.Password == loginEntity.password).FirstOrDefault();
                    if (member != null)
                    {
                        var token = new AccountToken();
                        token.Token = Common.Infrastructure.CreateToken();
                        token.AccountType = (int)Medical.AccountType.MemberType;
                        token.Account = member.Mobile;
                        token.CreatorTime = DateTime.Now;
                        db.Entry<AccountToken>(token).State = System.Data.Entity.EntityState.Added;
                        db.AccountTokens.Add(token);
                        db.SaveChanges();
                        var data = new
                        {
                            member = member,
                            token = token.Token
                        };
                        result.Data = data;
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

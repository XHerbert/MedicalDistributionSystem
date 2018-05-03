//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/3 11:52:45
//  文件名：LoginController
//  版本：V1.0.0 
//  说明：
//==============================================================
using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using Newtonsoft.Json;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static MedicalDistributionSystem.Domain.Enums.Medical;

namespace MedicalDistributionSystem.Api.Filters
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //记录请求参数
            var args = JsonConvert.SerializeObject(actionContext.ActionArguments);
            if (Infrastructure.GetConfig("OpenLog").Equals("True"))
            {
                using (var db = new MDDbContext())
                {
                    Log currentRequest = new Log();
                    currentRequest.LogType = (int)LogType.Parameter;
                    currentRequest.LogUrl = actionContext.Request.RequestUri.AbsoluteUri;
                    currentRequest.LogContent = args;
                    db.Logs.Add(currentRequest);
                    db.Entry<Log>(currentRequest).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
            }
            if (actionContext.ActionDescriptor.ActionName.Contains("Login"))
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var tokenKeyvalue = actionContext.Request.Headers.Where(p => p.Key.ToLower() == "token").FirstOrDefault();
                if (!Infrastructure.IsTestEnvironment())
                {
                    if (tokenKeyvalue.Value != null && tokenKeyvalue.Value.Count() > 0)
                    {
                        string sqlQuery = $"SELECT TOP 1 Token FROM dbo.AccountToken WHERE Token='{tokenKeyvalue.Value.FirstOrDefault()}' AND DeleteMark =0";
                        using (var db = new MDDbContext())
                        {
                            DbRawSqlQuery<string> res = db.Database.SqlQuery<string>(sqlQuery);
                            if (res.Count() == 0 || string.IsNullOrEmpty(res.First()))
                            {
                                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                                actionContext.Response.ReasonPhrase = "Token是假的，不允许访问";
                                return;
                            }
                        }
                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                        actionContext.Response.ReasonPhrase = "没有Token，不允许访问";
                        return;
                    }
                }
                base.OnActionExecuting(actionContext);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
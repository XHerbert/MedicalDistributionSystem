using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MedicalDistributionSystem.Api.Filters
{
    public class ValidateAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //记录请求参数
            var args = JsonConvert.SerializeObject(actionContext.ActionArguments);
            if (actionContext.ActionDescriptor.ActionName.Contains("Login"))
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var tokenKeyvalue = actionContext.Request.Headers.Where(p => p.Key == "Token").FirstOrDefault();
                if (!Common.Infrastructure.IsTestEnvironment())
                {
                    if (tokenKeyvalue.Value != null)
                    {


                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                        actionContext.Response.ReasonPhrase = "没有Token，不允许访问";
                        //actionContext.Response.Content = 
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
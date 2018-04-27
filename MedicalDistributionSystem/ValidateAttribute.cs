using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MedicalDistributionSystem
{
    public class ValidateAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var tokenKeyvalue= actionContext.Request.Headers.Where(p => p.Key == "Token").FirstOrDefault();
            if(tokenKeyvalue.Value == null)
            {
                return;
            }
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
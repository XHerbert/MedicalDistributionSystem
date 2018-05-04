//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/4 13:01:41
//  文件名：CustomFilterAttribute
//  版本：V1.0.0 
//  说明：  
//==============================================================
using MedicalDistributionSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Attribute
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.ActionDescriptor.ActionName.Contains("Login")|| filterContext.ActionDescriptor.ActionName.Contains("Logout"))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            if (null == OperatorProvider.Provider.GetCurrent())
            {
                filterContext.HttpContext.Response.Redirect("/Login/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
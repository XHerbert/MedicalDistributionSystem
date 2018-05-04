//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/4 10:50:18
//  文件名：LoginController
//  版本：V1.0.0 
//  说明：  
//==============================================================
using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using MedicalDistributionSystem.Domain.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult DoLogin(string account,string password)
        {
            if (account == null)
            {
                return Redirect("/Login/Login");
            }
            using (var db = new MDDbContext())
            {
                Expression<Func<Proxy, bool>> where = (p => p.DeleteMark == false && (p.ProxyName == account.Trim() || p.Mobile == account.Trim()) && MD5Helper.VerifyMd5Hash(password, p.Password));
                var user = db.Proxies.Where(where.Compile()).First();
                if (user != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = user.Id;
                    operatorModel.UserName = user.ProxyName;
                    operatorModel.UserPwd = user.Password;
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.Role = Infrastructure.GetProxyLevel(user.ProxyLevel);
                    operatorModel.IsAdmin = (user.ProxyLevel == (int)Medical.ProxyLevelEnums.LevelSuper);
                    operatorModel.Level = user.ProxyLevel;
                    Session["User"] = operatorModel;
                    Session["UserName"] = user.ProxyName;
                    Session["Level"] = user.ProxyLevel;
                    OperatorProvider.Provider.AddCurrent(operatorModel);
                    return Redirect("/Proxy/Index");
                }
            }
            return Redirect("/Login/Login");
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult DoLogout()
        {
            OperatorProvider.Provider.RemoveCurrent();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
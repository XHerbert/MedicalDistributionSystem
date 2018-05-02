using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Controllers
{
    /// <summary>
    /// 代理
    /// </summary>
    public class ProxyController : BaseController
    {
        /// <summary>
        /// 代理列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 创建会员(由代理操作)
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Proxy proxy)
        {
            ApiResult<Proxy> result = new ApiResult<Proxy>();
            using (var db = new MDDbContext())
            {
                //proxy.Create();
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Added;
                db.Proxies.Add(proxy);
                db.SaveChanges();
                //result.Data = proxy;
            }

            return Json(result);
        }

        /// <summary>
        /// 删除代理
        /// </summary>
        /// <param name="proxyId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int proxyId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var proxy = db.Proxies.Find(proxyId);
                //proxy.Remove();
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                //result.Data = true;
            }
            return Json(result);
        }
    }
}
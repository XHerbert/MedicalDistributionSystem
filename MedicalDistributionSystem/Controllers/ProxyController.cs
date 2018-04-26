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
        public ActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<Proxy>> result = new ApiResult<IList<Proxy>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Proxies where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (list == null)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.IsSuccess = false;
                    result.Msg = "";
                }
                else
                {
                    result.Data = list;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
                proxy.Create();
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Added;
                db.Proxies.Add(proxy);
                db.SaveChanges();
                result.Data = proxy;
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
                proxy.Remove();
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                result.Data = true;
            }
            return Json(result);
        }
    }
}
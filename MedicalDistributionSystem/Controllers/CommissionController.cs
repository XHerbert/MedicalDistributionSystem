using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Controllers
{
    /// <summary>
    /// 佣金
    /// </summary>
    public class CommissionController : Controller
    {
        /// <summary>
        /// 佣金列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int proxyId,int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<Commission>> result = new ApiResult<IList<Commission>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Commissions where (u.ProxyId==proxyId && u.DeleteMark == false) orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        /// 创建佣金记录
        /// </summary>
        /// <param name="commission"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Commission commission)
        {
            ApiResult<Commission> result = new ApiResult<Commission>();
            using (var db = new MDDbContext())
            {
                //commission.Create();
                db.Entry<Commission>(commission).State = System.Data.Entity.EntityState.Added;
                db.Commissions.Add(commission);
                db.SaveChanges();
                result.Data = commission;
            }

            return Json(result);
        }

        /// <summary>
        /// 删除佣金流水
        /// </summary>
        /// <param name="commissionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int commissionId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var commission = db.Commissions.Find(commissionId);
                //commission.Remove();
                db.Entry<Commission>(commission).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                result.Data = true;
            }
            return Json(result);
        }
    }
}
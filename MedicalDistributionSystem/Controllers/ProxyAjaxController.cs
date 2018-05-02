using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Controllers
{
    public class ProxyAjaxController : Controller
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult GetList(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<Proxy>> result = new ApiResult<IList<Proxy>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Proxies where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (list == null || list.Count==0)
                {
                    result.Code = 500;
                    result.Data = list;
                    result.Msg = "暂无数据";
                }
                else
                {
                    result.Data = list;
                    result.Code = 200;
                    result.Count = db.Proxies.Count(p=>p.DeleteMark==false);
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Proxies where u.DeleteMark == false && u.CreatorUserId==id orderby u.Id ascending select u).ToList();
                if (list == null || list.Count==0)
                {
                    var proxy = db.Proxies.Find(id);
                    proxy.DeleteMark = true;
                    proxy.Modify(id);
                    db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    result.Data = true;

                }
                else
                {
                    result.Data = false;
                    result.Code = 500;
                    result.Msg = "存在引用，不能删除";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
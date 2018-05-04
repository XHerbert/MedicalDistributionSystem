//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/1 11:52:45
//  文件名：LoginController
//  版本：V1.0.0 
//  说明：
//==============================================================
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalDistributionSystem.Common;

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
        /// 创建代理
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Proxy proxy)
        {
            ApiResult<Proxy> result = new ApiResult<Proxy>();
            using (var db = new MDDbContext())
            {
                var model = OperatorProvider.Provider.GetCurrent();
                proxy.Create(model.UserId);
                proxy.ProxyLevel = model.Level+1;
                proxy.Password = MD5Helper.GetMd5Hash(Resource.DEFAULT_PASSWORD);
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Added;
                db.Proxies.Add(proxy);
                db.SaveChanges();
                result.Data = proxy;
            }
            return Json(result);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public ActionResult Update(Proxy proxy)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                
                if (proxy != null && proxy.Id>0)
                {
                    var currentProxy = db.Proxies.Find(proxy.Id);
                    currentProxy.ProxyName = proxy.ProxyName;
                    currentProxy.Mobile = proxy.Mobile;
                    currentProxy.Province = proxy.Province;
                    currentProxy.City = proxy.City;
                    currentProxy.BackMoneyPercent = proxy.BackMoneyPercent;
                    currentProxy.Modify(OperatorProvider.Provider.GetCurrent().UserId);
                    db.Entry<Proxy>(currentProxy).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    result.Data = true;
                }
                else
                {
                    result.Data = false;
                    result.Code = 500;
                    result.Msg = "更新异常";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
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
                    proxy.Modify(OperatorProvider.Provider.GetCurrent().UserId);
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

        public ActionResult Search(Proxy proxy)
        {
            ApiResult<IList<Proxy>> result = new ApiResult<IList<Proxy>>();
            using (var db = new MDDbContext())
            {
                var list = (from  u in db.Proxies
                            where u.DeleteMark == false && (
                                  u.ProxyName.Contains(proxy.ProxyName) ||
                                  u.Mobile.Contains(proxy.Mobile)) orderby u.Id ascending select u).ToList();



                if (list.Count > 0)
                {
                    result.Data = list;
                    result.Count = list.Count;
                }
                else
                {
                    result.Data = new List<Proxy>();
                    result.Code = 404;
                    result.Msg = "没有符合条件的记录";
                }
            }
            return Json(result);
        }
    }
}
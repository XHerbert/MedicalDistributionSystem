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
    /// 硬件设备
    /// </summary>
    public class HardWareController : BaseController
    {
        /// <summary>
        /// 硬件设备列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<HardWare>> result = new ApiResult<IList<HardWare>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.HardWares where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (list == null)
                {
                    result.Code = 500;
                    result.Data = null;
                    //result.IsSuccess = false;
                    result.Msg = "";
                }
                else
                {
                    //result.Data = list;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建硬件设备
        /// </summary>
        /// <param name="hardWare"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(HardWare hardWare)
        {
            ApiResult<HardWare> result = new ApiResult<HardWare>();
            using (var db = new MDDbContext())
            {
                //hardWare.Create();
                db.Entry<HardWare>(hardWare).State = System.Data.Entity.EntityState.Added;
                db.HardWares.Add(hardWare);
                db.SaveChanges();
                //result.Data = hardWare;
            }

            return Json(result);
        }

        /// <summary>
        /// 更新设备硬件
        /// </summary>
        /// <param name="hardWare"></param>
        /// <returns></returns>
        public ActionResult Modify(HardWare hardWare)
        {
            ApiResult<HardWare> result = new ApiResult<HardWare>();
            using (var db = new MDDbContext())
            {
                var hardWareId = hardWare.Id;
                var queryHardWare = db.HardWares.Find(hardWareId);
                queryHardWare.UsedSeconds = hardWare.UsedSeconds;
                queryHardWare.UseableSeconds = hardWare.UseableSeconds;
                //queryHardWare.Modify();
                db.Entry<HardWare>(queryHardWare).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(result);
        }

        /// <summary>
        /// 删除硬件设备
        /// </summary>
        /// <param name="hardWareId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int hardWareId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var hardWare = db.HardWares.Find(hardWareId);
                //hardWare.Remove();
                db.Entry<HardWare>(hardWare).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                //result.Data = true;
            }
            return Json(result);
        }
    }
}
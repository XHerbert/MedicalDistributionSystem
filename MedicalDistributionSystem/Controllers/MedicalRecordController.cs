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
    /// 病历（消费记录）
    /// </summary>
    public class MedicalRecordController : Controller
    {
        /// <summary>
        /// 消费记录列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<MedicalRecord>> result = new ApiResult<IList<MedicalRecord>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.MedicalRecords where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        /// 创建消费记录
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(MedicalRecord medicalRecord)
        {
            ApiResult<MedicalRecord> result = new ApiResult<MedicalRecord>();
            using (var db = new MDDbContext())
            {
                //medicalRecord.Create();
                db.Entry<MedicalRecord>(medicalRecord).State = System.Data.Entity.EntityState.Added;
                db.MedicalRecords.Add(medicalRecord);
                db.SaveChanges();
                //result.Data = medicalRecord;
            }

            return Json(result);
        }

        /// <summary>
        /// 删除消费记录
        /// </summary>
        /// <param name="medicalRecordId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int medicalRecordId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var medicalRecord = db.MedicalRecords.Find(medicalRecordId);
                //medicalRecord.Remove();
                db.Entry<MedicalRecord>(medicalRecord).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                //result.Data = true;
            }
            return Json(result);
        }
    }
}
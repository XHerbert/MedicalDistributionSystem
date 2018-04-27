﻿using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MedicalDistributionSystem.Api.Controllers
{
    public class MedicalRecordController : BaseController
    {
        /// <summary>
        /// 获取消费记录/病历（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IList<MedicalRecord>> Get(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<MedicalRecord>> result = new ApiResult<IList<MedicalRecord>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.MedicalRecords where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (list == null)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                }
                else
                {
                    result.Data = list;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取指定消费记录/病历
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<MedicalRecord> GetSingle(int id)
        {
            ApiResult<MedicalRecord> result = new ApiResult<MedicalRecord>();
            using (var db = new MDDbContext())
            {
                var medicalRecord = db.MedicalRecords.Find(id);
                if (null == medicalRecord)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = medicalRecord;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建消费记录/病历
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<MedicalRecord> Create(MedicalRecord medicalRecord)
        {
            ApiResult<MedicalRecord> result = new ApiResult<MedicalRecord>();
            using (var db = new MDDbContext())
            {
                medicalRecord.Create();
                db.Entry<MedicalRecord>(medicalRecord).State = System.Data.Entity.EntityState.Added;
                db.MedicalRecords.Add(medicalRecord);
                db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除消费记录/病历
        /// </summary>
        /// <param name="medicalRecordId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(int medicalRecordId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var medicalRecord = db.MedicalRecords.Find(medicalRecordId);
                if (medicalRecord == null)
                {
                    result.Data = false;
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    return result;
                }
                medicalRecord.Remove();
                db.Entry<MedicalRecord>(medicalRecord).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}
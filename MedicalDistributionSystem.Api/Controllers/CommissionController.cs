//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/27 9:52:45
//  文件名：CommissionController
//  版本：V1.0.0 
//  说明：
//==============================================================
using MedicalDistributionSystem.Api.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MedicalDistributionSystem.Api.Controllers
{
    public class CommissionController : BaseController
    {
        /// <summary>
        /// 获取佣金流水（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IList<Commission>> Get(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<Commission>> result = new ApiResult<IList<Commission>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Commissions where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        /// 获取指定佣金流水
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Commission> GetSingle(int id)
        {
            ApiResult<Commission> result = new ApiResult<Commission>();
            using (var db = new MDDbContext())
            {
                var commission = db.Commissions.Find(id);
                if (null == commission)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = commission;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建佣金流水
        /// </summary>
        /// <param name="commission"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Commission> Create(Commission commission)
        {
            ApiResult<Commission> result = new ApiResult<Commission>();
            using (var db = new MDDbContext())
            {
                commission.Create();
                db.Entry<Commission>(commission).State = System.Data.Entity.EntityState.Added;
                db.Commissions.Add(commission);
                db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除佣金流水
        /// </summary>
        /// <param name="commissionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(int commissionId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var commission = db.Commissions.Find(commissionId);
                if (commission == null)
                {
                    result.Data = false;
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    return result;
                }
                commission.Remove();
                db.Entry<Commission>(commission).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}

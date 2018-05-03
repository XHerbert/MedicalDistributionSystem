//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/27 9:52:45
//  文件名：ProxyController
//  版本：V1.0.0 
//  说明：
//==============================================================
using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MedicalDistributionSystem.Api.Controllers
{
    public class HardWareController : BaseController
    {
        /// <summary>
        /// 获取设备列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IList<HardWare>> Get(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<HardWare>> result = new ApiResult<IList<HardWare>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.HardWares where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        /// 获取指定设备硬件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<HardWare> GetSingle(int id)
        {
            ApiResult<HardWare> result = new ApiResult<HardWare>();
            using (var db = new MDDbContext())
            {
                var hardWare = db.HardWares.Find(id);
                if (null == hardWare)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = hardWare;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建设备
        /// </summary>
        /// <param name="hardWare"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<HardWare> Create(HardWare hardWare)
        {
            ApiResult<HardWare> result = new ApiResult<HardWare>();
            using (var db = new MDDbContext())
            {
                hardWare.Create(hardWare.ProxyId);
                db.Entry<HardWare>(hardWare).State = System.Data.Entity.EntityState.Added;
                db.HardWares.Add(hardWare);
                db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除设备
        /// </summary>
        /// <param name="hardWareId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(int hardWareId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var hardWare = db.HardWares.Find(hardWareId);
                if (hardWare == null)
                {
                    result.Data = false;
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    return result;
                }
                hardWare.Remove(hardWare.ProxyId);
                db.Entry<HardWare>(hardWare).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}

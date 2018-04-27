//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/26 9:52:45
//  文件名：ProxyController
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
    /// <summary>
    /// 
    /// </summary>
    public class ProxyController : BaseController
    {
        /// <summary>
        /// 获取代理列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IList<Proxy>> Get(int pageIndex = 1, int pageSize = 10)
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
        /// 获取指定代理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Proxy> GetSingle(int id)
        {
            ApiResult<Proxy> result = new ApiResult<Proxy>();
            using (var db = new MDDbContext())
            {
                var proxy = db.Proxies.Find(id);
                if (null == proxy)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = proxy;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建代理(由上级代理操作)
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Proxy> Create(Proxy proxy)
        {
            ApiResult<Proxy> result = new ApiResult<Proxy>();
            using (var db = new MDDbContext())
            {
                string md5 = MD5Helper.GetMd5Hash(proxy.Password);
                proxy.Password = md5;
                proxy.Create();
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Added;
                db.Proxies.Add(proxy);
                db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除代理
        /// </summary>
        /// <param name="proxyId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(int proxyId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var proxy = db.Proxies.Find(proxyId);
                if (proxy == null)
                {
                    result.Data = false;
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    return result;
                }
                proxy.Remove();
                db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}

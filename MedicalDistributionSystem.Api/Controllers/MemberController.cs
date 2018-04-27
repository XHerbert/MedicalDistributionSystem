//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/26 9:52:45
//  文件名：MemberController
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
    /// <summary>
    /// 
    /// </summary>
    public class MemberController : BaseController
    {
        /// <summary>
        /// 获取会员列表（分页）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IList<Member>> Get(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<Member>> result = new ApiResult<IList<Member>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Members where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        /// 获取指定会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Member> GetSingle(int id)
        {
            ApiResult<Member> result = new ApiResult<Member>();
            using (var db= new MDDbContext())
            {
                var member = db.Members.Find(id);
                if (null == member)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = member;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建会员(由代理操作)
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Member> Create(Member member)
        {
            ApiResult<Member> result = new ApiResult<Member>();
            using (var db = new MDDbContext())
            {
                member.Create();
                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Added;
                db.Members.Add(member);
                db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除会员
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(int memberId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var member = db.Members.Find(memberId);
                if (member == null)
                {
                    result.Data = false;
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    return result;
                }
                member.Remove();
                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }
    }
}

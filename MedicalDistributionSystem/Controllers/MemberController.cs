using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Controllers
{
    /// <summary>
    /// 会员业务
    /// </summary>
    [Validate]
    public class MemberController : BaseController
    {
        /// <summary>
        /// 会员列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int pageIndex = 1,int pageSize = 10)
        {
            ApiResult<IList<Member>> result = new ApiResult<IList<Member>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Members where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if(list == null)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.IsSuccess = false;
                    result.Msg = "";
                }else
                {
                    result.Data = list;
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建会员(由代理操作)
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Member member)
        {
            ApiResult<Member> result = new ApiResult<Member>();
            using (var db = new MDDbContext())
            {
                //member.Create();
                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Added;
                db.Members.Add(member);
                db.SaveChanges();
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(int memberId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var member = db.Members.Find(memberId);
                //member.Remove();
                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Json(result);
        }
    }
}
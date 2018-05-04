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
    public class MemberAjaxController : Controller
    {


        /// <summary>
        /// 会员列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetList(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<Member>> result = new ApiResult<IList<Member>>();
            var loginUser = OperatorProvider.Provider.GetCurrent();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.Members where (u.DeleteMark == false && u.CreatorUserId == loginUser.UserId) orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (list.Count == 0)
                {
                    result.Code = 404;
                    result.Data = new List<Member>();
                    result.IsSuccess = true;
                    result.Msg = "未找到相关记录";
                }
                else
                {
                    result.Data = list;
                    result.Count = db.Members.Count(p=>p.DeleteMark==false && p.CreatorUserId==loginUser.UserId);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
            var loginUser = OperatorProvider.Provider.GetCurrent();
            using (var db = new MDDbContext())
            {
                member.Create(loginUser.UserId);
                member.ProxyId = loginUser.UserId;
                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Added;
                db.Members.Add(member);
                db.SaveChanges();
                result.Data = member;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            var loginUser = OperatorProvider.Provider.GetCurrent();
            using (var db = new MDDbContext())
            {
                var member = db.Members.Find(Id);
                member.Remove(loginUser.UserId);
                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result.Data = true;
            }
            return Json(result);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult Update(Member member)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                if (member != null && member.Id > 0)
                {
                    var currentMember = db.Members.Find(member.Id);
                    currentMember.MemberName = member.MemberName;
                    currentMember.Mobile = member.Mobile;
                    currentMember.Location = member.Location;
                    currentMember.CardID = member.CardID;
                    currentMember.Gender = member.Gender;
                    currentMember.Remark = member.Remark;
                    currentMember.Modify(OperatorProvider.Provider.GetCurrent().UserId);
                    db.Entry<Member>(currentMember).State = System.Data.Entity.EntityState.Modified;
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
    }
}
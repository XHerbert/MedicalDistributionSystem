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
    public class MemberController : BaseController
    {
        /// <summary>
        /// 会员列表(分页)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 创建会员(由代理操作)
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var db = new MDDbContext())
            {
                var entity = db.Members.Find(id);
                if (entity != null)
                {
                    //ViewBag.ProxyLevel = Infrastructure.GetProxyLevel(entity.ProxyLevel);
                    if (entity.CreatorUserId != 0)
                    {
                        ViewBag.Parent = db.Proxies.Find(entity.CreatorUserId).ProxyName;
                    }
                }
                return View(entity);
            }
        }

        public ActionResult ViewData(int id)
        {
            using (var db = new MDDbContext())
            {
                var entity = db.Members.Find(id);
                if (entity != null)
                {
                    //ViewBag.ProxyLevel = Infrastructure.GetProxyLevel(entity.ProxyLevel);
                    if (entity.CreatorUserId != 0)
                    {
                        ViewBag.Parent = db.Proxies.Find(entity.CreatorUserId).ProxyName;
                    }
                }
                return View(entity);
            }
        }
    }
}
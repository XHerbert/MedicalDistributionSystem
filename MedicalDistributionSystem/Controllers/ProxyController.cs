using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalDistributionSystem.Common;

namespace MedicalDistributionSystem.Controllers
{
    /// <summary>
    /// 代理
    /// </summary>
    public class ProxyController : BaseController
    {
        /// <summary>
        /// 代理列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var db = new MDDbContext())
            {
                var entity = db.Proxies.Find(id);
                if(entity != null)
                {
                    ViewBag.ProxyLevel = Infrastructure.GetProxyLevel(entity.ProxyLevel);
                    if(entity.CreatorUserId != 0)
                    {
                        ViewBag.Parent = db.Proxies.Find(entity.CreatorUserId).ProxyName;
                    }
                }
                return View(entity);
            }
        }

       
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ViewData(int id)
        {
            using (var db = new MDDbContext())
            {
                var entity = db.Proxies.Find(id);
                if (entity != null)
                {
                    ViewBag.ProxyLevel = Infrastructure.GetProxyLevel(entity.ProxyLevel);
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
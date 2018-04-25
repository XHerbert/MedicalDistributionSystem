using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalDistributionSystem.Controllers
{
    /// <summary>
    /// 会员业务
    /// </summary>
    public class MemberController : Controller
    {
        /// <summary>
        /// 会员列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var d = new { A="sa",B= "DS"};
            return Json(d);
        }
    }
}
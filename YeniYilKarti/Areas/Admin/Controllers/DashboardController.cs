using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using YeniYilKarti.Helpers;
using YeniYilKarti.Models;

namespace YeniYilKarti.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : AdminBaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.XYZ = user.DisplayName;
            return View(db.Kartlar.Where(x=>x.UserId == user.Id).ToList());
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using Enteripse_web.CustomFilters;
using Enteripse_web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Enteripse_web.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        [AuthLog(Roles = "Administrator")]
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

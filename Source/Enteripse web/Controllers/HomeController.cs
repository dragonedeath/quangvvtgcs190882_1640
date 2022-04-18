using Enteripse_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using Enteripse_web.CustomFilters;

namespace Enteripse_web.Controllers
{
    public class HomeController : BaseController
    {
        protected ApplicationDbContext data = new ApplicationDbContext();


        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            
                var posts = data.Posts.ToList();
                var page_post = posts.OrderByDescending(x=> x.Time).ToPagedList(page, pageSize);
                return View(page_post);
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Fail()
        {
            return View();
        }
        public ActionResult FailComment()
        {
            return View("FailComment");
        }
        public ActionResult FailDelete()
        {
            return View("FailDelete");
        }
    }
}
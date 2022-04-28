using Enteripse_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enteripse_web.Controllers
{
    public class StaticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Statics
        public ActionResult Index()
        {
            var total_post = db.Posts.Count();
            decimal num_post_QA = 0;
            decimal num_post_IT = 0;
            decimal num_post_Business = 0;
            decimal num_post_HR = 0;
            var post = db.Posts.ToList();

            foreach (var post1 in post)
            {
                var user = db.Users.Where(x => x.Id == post1.AuthorId).FirstOrDefault();
                if(user.Department.Name == "QA")
                {
                    num_post_QA++;
                }
                else if(user.Department.Name =="IT")
                {
                    num_post_IT++;
                }
                else if(user.Department.Name== "Bussiness")
                {
                    num_post_Business++;
                }
                else if(user.Department.Name == "Human Resources")
                {
                    num_post_HR++;
                }

            }
            var statitics = new StaticsViewModel();
            {
                statitics.numberPostofIT = num_post_IT;
                statitics.numberPostofQA = num_post_QA;
                statitics.numberPostofHR = num_post_HR;
                statitics.numberPostofBusiness = num_post_Business;
            }

            return View(statitics);
        }
    }
}
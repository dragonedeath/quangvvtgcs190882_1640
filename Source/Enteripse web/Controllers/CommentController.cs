using Enteripse_web.Models;
using Microsoft.AspNet.Identity;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Enteripse_web.Controllers
{
    public class CommentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        

    }
}
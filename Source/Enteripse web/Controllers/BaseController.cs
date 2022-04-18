using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enteripse_web.Controllers
{
    using Enteripse_web.Models;
    using System.Web.Mvc;
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
    }
}
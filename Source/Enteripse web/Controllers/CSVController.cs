using Enteripse_web.CustomFilters;
using Enteripse_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enteripse_web.Controllers
{
    [AuthLog(Roles = "Administrator")]
    public class CSVController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CSV
        public FileContentResult DownloadCSV()
        {
            string csv = "\"PostId\",\"Title\",\"Time\",\"AuthorId\",\"AuthorName\",\"Description\",\"DocumentName\" \n";
            var List = db.Posts.ToList(); //get this list from database 
            foreach (Post item in List)
            {
                csv = csv + String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\" \n",
                                           item.PostId,
                                           item.Title,
                                           item.Time,
                                           item.AuthorId,
                                           item.AuthorName,
                                           item.Description,
                                           item.DocumentName);
            }
            //StringWriter sw = new StringWriter();
            //sw.WriteLine
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Report123.csv");
        }
    }
}
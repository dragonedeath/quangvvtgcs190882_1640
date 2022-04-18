using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ionic.Zip;
using System.IO;
using Enteripse_web.Models;
using Enteripse_web.CustomFilters;

namespace Enteripse_web.Controllers
{
    public class DownloadController : Controller
    {
        [AuthLog(Roles = "Administrator")]
        public ActionResult Index()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Files/"));
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel()
                {
                    FileName = Path.GetFileName(filePath),
                    FilePath = filePath
                });
            }

            return View(files);
        }

        [HttpPost]
        public ActionResult Index(List<FileModel> files)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");
                foreach (FileModel file in files)
                {
                    if (file.IsSelected)
                    {
                        zip.AddFile(file.FilePath, "Files");
                    }
                }
                string zipName = String.Format("FilesZip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    zip.Save(memoryStream);
                    return File(memoryStream.ToArray(), "application/zip", zipName);
                }
            }


        }
    }
}
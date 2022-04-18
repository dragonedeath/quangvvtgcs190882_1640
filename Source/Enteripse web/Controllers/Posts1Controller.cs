using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Enteripse_web.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace Enteripse_web.Controllers
{
    public class Posts1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationDbContext context;
        public Posts1Controller()
        {
            context = new ApplicationDbContext();
        }

        // GET: Posts1
        public ActionResult Index()
        {  
            var posts = db.Posts.Include(p => p.Author);
            return View(posts.ToList());
        }
        [HttpGet]
        public ActionResult Comment(int id)
        {
   
            return PartialView("_PartialComment", new Enteripse_web.Models.Comment { PostId = id });
        }

        [HttpPost]
        public ActionResult Comment(Comment data, int id)
        {
            try
            {
                data.AuthorId = User.Identity.GetUserId();
                data.submissionId = id;
                var submit = db.Submissions.Find(id);
                if (DateTime.Now > submit.FinalDate)
                {
                    return RedirectToAction("FailComment", "Home");
                }
                else
                {


                    db.Comments.Add(data);
                    db.SaveChanges();
                }
                if (data.IsAnonymus == true)
                {
                    data.AuthorId = "Anonymous";
                }
                else
                {
                    data.AuthorId = data.AuthorId;
                }
                MailMessage mail = new MailMessage();
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                     | SecurityProtocolType.Tls11
                                                      | SecurityProtocolType.Tls12;
                }

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Credentials = new System.Net.NetworkCredential("thienlun15082001@gmail.com", "thien15082001");
                smtpServer.EnableSsl = true;
                smtpServer.Port = 587; // Gmail works on this port

                mail.From = new MailAddress("thienlun15082001@gmail.com");

                var userId = User.Identity.GetUserId();
                var post = db.Posts.Where(x => x.PostId == data.PostId).FirstOrDefault();
                var author = db.Users.Where(x => x.Id == post.AuthorId).FirstOrDefault();
                var email = author.Email;
                mail.To.Add(new MailAddress(email));
                mail.Subject = "New comment";
                mail.Body = "Your post has new comment. Please check it!";

                smtpServer.Send(mail);

               

                
                    return RedirectToAction("Index", "Home");
                

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }
        // GET: Posts1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (post.IsAnonymus == true)
                {
                    post.AuthorName = "Anonymus";
                }
                else
                {
                    post.AuthorName = post.Author.FullName;
                    }
            }
            return View(post);
        }

        // GET: Posts1/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.ToList(), "Id", "Name");
            return View();
        }
        

        // POST: Posts1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, int id)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = User.Identity.GetUserId();
                if (Request.Files != null)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Files"),
                                                   Path.GetFileName(file.FileName));
                        //Save file using Path+fileName take from above string
                        file.SaveAs(path);
                        post.DocumentName = Path.GetFileName(file.FileName);
                    }
                }
                MailMessage mail = new MailMessage();
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                      | SecurityProtocolType.Tls11
                                                      | SecurityProtocolType.Tls12;
                }

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Credentials = new System.Net.NetworkCredential("thienlun15082001@gmail.com", "thien15082001");
                smtpServer.EnableSsl = true;
                smtpServer.Port = 587; // Gmail works on this port

                mail.From = new MailAddress("thienlun15082001@gmail.com");

                var userId = User.Identity.GetUserId();
                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault(); //tim entity user
                if(user.Department.Name == "Human Resources")
                {
                    mail.To.Add("qahumanr@gmail.com");

                }
                if (user.Department.Name == "IT")
                {
                    mail.To.Add("qainformationt@gmail.com");

                }
                if (user.Department.Name == "Bussiness")
                {
                    mail.To.Add("qabussiness06@gmail.com");

                }
                if (user.Department.Name == "QA")
                {
                    mail.To.Add("thienlun26062001@gmail.com");

                }

                mail.Subject = "New post";
                mail.Body = "Your department has a new post. Please check it!";

                smtpServer.Send(mail);
                post.submissionId = id;
                var submit = db.Submissions.Find(id);
                if (DateTime.Now > submit.closureDate)
                {
                    return RedirectToAction("Fail", "Home");
                }
                else
                {


                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }
             

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", post.AuthorId);
            ViewBag.CategoryId = new SelectList(context.Categories, "Id", "Name", post.categoryId);
            return View(post);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {  //Server.MapPath takes the absolte path of folder 'Uploads'
                    string path = Path.Combine(Server.MapPath("~/Files"),
                                               Path.GetFileName(file.FileName));
                    //Save file using Path+fileName take from above string
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

    // GET: Posts1/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", post.AuthorId);
            return View(post);
        }

        // POST: Posts1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Time,AuthorId,Description,IsAnonymus")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FullName", post.AuthorId);
            return View(post);
        }

        // GET: Posts1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

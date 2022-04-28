using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enteripse_web.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string AuthorId  { get; set; }
        public virtual ApplicationUser Author   { get; set; }
        public bool IsAnonymus { get; set; }
        public int PostId { get; set; }
        public int submissionId { get; set; }

        public virtual Post Post { get; set; }
    }
}
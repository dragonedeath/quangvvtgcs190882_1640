using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Enteripse_web.Models
{
    public class Post
    {
        public Post()
        {
            this.IsAnonymus = true;
            this.Time = DateTime.Now;
            this.Comments = new HashSet<Comment>();
        }
        public int PostId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        [MaxLength(400)]
        public string AuthorName   { get; set; }
        public string Description { get; set; }
        public bool IsAnonymus { get; set; }
        
        public string DocumentName { get; set; }
        public int submissionId { get; set; }
        public int categoryId   { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
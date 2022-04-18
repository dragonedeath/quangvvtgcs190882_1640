using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Enteripse_web.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Enteripse_web.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Enteripse_web.Models.Submission> Submissions { get; set; }

        public System.Data.Entity.DbSet<Enteripse_web.Models.Category> Categories { get; set; }

        //public System.Data.Entity.DbSet<Enteripse_web.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
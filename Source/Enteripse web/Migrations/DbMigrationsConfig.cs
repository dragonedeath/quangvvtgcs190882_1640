namespace Enteripse_web.Migrations
{
    using Enteripse_web.Models;
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class DbMigrationsConfig : DbMigrationsConfiguration<Enteripse_web.Models.ApplicationDbContext>
    {

        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(Enteripse_web.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                CreateDepartment(context);

                var adminEmail = "admin@gmail.com";
                var adminUserName = adminEmail;
                var adminFullName = "Admin";
                var adminPassword = adminEmail;
                var adminRole = "Administrator";
                var adminDepartment = 1;

                CreateAdminUser(context, adminEmail, adminUserName, adminFullName, adminPassword, adminRole, adminDepartment);
            }
        }

        private void CreateDepartment(ApplicationDbContext context)
        {
            context.Departments.Add(new Department()
            {
                DepartmentId = 1,
                Name = "QA"
            });
        }

        private void CreateAdminUser(ApplicationDbContext context, string adminEmail, string adminUserName, string adminFullName, string adminPassword, string adminRole, int adminDepartment)
        {
            // Tao "admin"
            var adminUser = new ApplicationUser()
            {
                UserName = adminUserName,
                FullName = adminFullName,
                Email = adminEmail,
                DepartmentId = adminDepartment
            };
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var userCreateResult = userManager.Create(adminUser, adminPassword);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            var addTrainingstaffRoleResult = userManager.AddToRole(adminUser.Id, adminRole);
            if (!addTrainingstaffRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addTrainingstaffRoleResult.Errors));
            }

        }

    }
           
}

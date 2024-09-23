using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;

using ShopDoNu.Identity;

[assembly: OwinStartup(typeof(ShopDoNu.Startup))]

namespace ShopDoNu
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login")
            });
            this.CreateRolesAndUser();
        }
        public void CreateRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDBContext()));
            var appDbContext = new AppDBContext();
            var appUserStore = new AppUserStore(appDbContext);
            var usermanager = new AppUserManager(appUserStore);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                var roleResult = roleManager.Create(role);
            }

            if(usermanager.FindByName("admin") == null)
            {
                var user = new AppUser();
                user.UserName = "thevinhadmin";
                user.Email = "nobi2612@gmail.com";
                user.PhoneNumber = "0345578045";
                user.Address = "Admin";
                string userPwd = "admin123";

                var chkUser = usermanager.Create(user, userPwd);

                if(chkUser.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "Admin");
                }
            }
            //==================Manager======================================================
            //if (!roleManager.RoleExists("Manager"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "Manager";
            //    roleManager.Create(role);
            //}

            //if (usermanager.FindByName("manager") == null)
            //{
            //    var user = new AppUser();
            //    user.UserName = "thevinhmanager";
            //    user.Email = "thevinhmanager@gmail.com";
            //    user.PhoneNumber = "0200121092";
            //    user.Address = "TP. Hồ Chí Minh";
            //    string userPwd = "manager";

            //    var chkUser = usermanager.Create(user, userPwd);
            //    if (chkUser.Succeeded)
            //    {
            //        usermanager.AddToRole(user.Id, "Manager");
            //    }
            //}
            //==========================================================================
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}

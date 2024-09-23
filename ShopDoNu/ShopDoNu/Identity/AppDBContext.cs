using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity.EntityFramework;
namespace ShopDoNu.Identity
{
    //, AppRole, null, AppUserLogin, AppUserRole, AppUserClaim
    public class AppDBContext: IdentityDbContext<AppUser>
    {
        public AppDBContext(): base("ShopDoNuDB") { }
    }
}
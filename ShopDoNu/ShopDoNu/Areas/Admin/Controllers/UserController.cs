using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDoNu.Identity;
using System.Threading.Tasks;
using ShopDoNu.Filters;
using ShopDoNu.Models;
using ShopDoNu.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Helpers;
namespace ShopDoNu.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class UserController : Controller
    {
        // GET: Admin/User
        AppDBContext db = new AppDBContext();
        public ActionResult Index()
        {
            List<AppUser> us = db.Users.ToList();
            return View(us);
        }
        public ActionResult DeleteUser(string id)
        {
            AppUser us = db.Users.Where(t => t.Id == id).FirstOrDefault();
            return View(us);
        }
        [HttpPost]
        public ActionResult DeleteUser(string id, AppUser us)
        {
            AppUser p = db.Users.Where(t => t.Id == id).FirstOrDefault();
            db.Users.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }
        //===================================================================================
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(RegisterVM rvm)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDBContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var passwdHash = Crypto.HashPassword(rvm.Password);
                var user = new AppUser()
                {
                    Email = rvm.Email,
                    UserName = rvm.Username,
                    PasswordHash = passwdHash,
                    PhoneNumber = rvm.PhoneNumber,
                    Address = rvm.Address

                };
                IdentityResult identityResult = userManager.Create(user);
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                }
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("Lỗi mới", "Dữ liệu không hợp lệ");
                return View();
            }

        }
        //===================================================================
        public ActionResult UpdateUser(string id)
        {

            AppUser us = db.Users.Where(t => t.Id == id).FirstOrDefault();

            ViewBag.Name = us.UserName;
            ViewBag.Emai = us.Email;
            ViewBag.PhoneNumber = us.PhoneNumber;
            ViewBag.Address = us.Address;

            return View(us);
        }

        [HttpPost]
        public ActionResult UpdateUser(string id, AppUser us)
        {
            if(ModelState.IsValid)
            {
                AppUser u = db.Users.Where(t => t.Id == id).FirstOrDefault();
                u.UserName = us.UserName;
                u.Email = us.Email;
                u.PhoneNumber = us.PhoneNumber;
                u.Address = us.Address;
                db.SaveChanges();
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            else
            {
                return View();
            }
        }


    }
}
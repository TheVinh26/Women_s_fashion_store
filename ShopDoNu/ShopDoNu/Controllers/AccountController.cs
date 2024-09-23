using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDoNu.Models;
using ShopDoNu.ViewModel;
using ShopDoNu.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using ShopDoNu.Library;
namespace ShopDoNu.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM rvm)
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
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);

                    /// Send registration confirmation email using SendGrid
                    var emailSender = new EmailSender();
                    await emailSender.SendEmailAsync(user.Email, "Registration Successful", "Thank you for registering with us! Your account has been successfully created.");

                    // Redirect users to a confirmation page or take other steps
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid data!");
                return View();
            }
        }
        public ActionResult ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                // Handle cases where userId or code does not exist
                return View("Error");
            }

            var userManager = new AppUserManager(new AppUserStore(new AppDBContext()));
            var user = userManager.FindById(userId);

            if (user == null)
            {
                // Handle cases where user does not exist
                return View("Error");
            }

            var result = userManager.ConfirmEmail(userId, code);

            if (result.Succeeded)
            {
                // Email confirm Success
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Email Confirm falied
                return RedirectToAction("Register", "Account");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM lvm)
        {
            var appDbContext = new AppDBContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.Find(lvm.Username, lvm.Password);
            if(user != null)
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                if(userManager.IsInRole(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }                
            }
            else
            {
                ModelState.AddModelError("myError", "Email or Password Invalid!");
                return View();
            }
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ProFile(AppUser us)
        {
            ViewBag.UserName = us.UserName;
            ViewBag.Email = us.Email;
            return View();
        }
    }
}
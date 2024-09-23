using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDoNu.Models;
namespace ShopDoNu.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        ShopDoNuContext db = new ShopDoNuContext();
        public ActionResult Index()
        {
            List<Category> c = db.Categories.ToList();
            return View(c);
        }
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        //==========================================================================
        public ActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Where(t => t.Id == id).FirstOrDefault();
            ViewBag.Name = category.Name;
            return View(category);
        }
        [HttpPost]
        public ActionResult DeleteCategory(int id, Category c)
        {
            Category category = db.Categories.Where(t => t.Id == id).FirstOrDefault();
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
    }
}
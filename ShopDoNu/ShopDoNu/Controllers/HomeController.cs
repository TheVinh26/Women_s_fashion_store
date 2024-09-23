using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDoNu.Models;
namespace ShopDoNu.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ShopDoNuContext db = new ShopDoNuContext();
        public ActionResult Index(int page = 1)
        {
            //throw new Exception("Error Home!");
            List<Product> pro = db.Products.ToList();
            int hien_thi = 8;
            int so_luong_trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pro.Count) /
                Convert.ToDouble(hien_thi)));
            int emp_can_skip = (page - 1) * hien_thi;
            ViewBag.Page = page;
            ViewBag.So_luong_trang = so_luong_trang;
            pro = pro.Skip(emp_can_skip).Take(hien_thi).ToList();
            return View(pro);
        }
        public ActionResult Error404()
        {
            return View();
        }
    }
}
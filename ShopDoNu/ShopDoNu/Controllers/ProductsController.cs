using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShopDoNu.Filters;
using ShopDoNu.Models;
namespace ShopDoNu.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        ShopDoNuContext db = new ShopDoNuContext();
        [MyAuthenFilter]
        //[MyExceptionFilter]
        public ActionResult Index(string search = "", string loai = "", string sortPrice = "", int page = 1)
        {
            //throw new Exception("Error Products!");
            List<Product> pro = db.Products.Where(i => i.Name.Contains(search)).ToList();
            ViewBag.Search = search;

            switch (loai)
            {
                case "dda":
                    pro = db.Products.Where(i => i.CategoryId == 2).ToList();
                    break;
                case "ap":
                    pro = db.Products.Where(i => i.CategoryId == 1).ToList();
                    break;
                case "sm":
                    pro = db.Products.Where(i => i.CategoryId == 3).ToList();
                    break;
                default:
                    break;
            }
            switch (sortPrice)
            {
                case "1":
                    pro = pro.Where(r => r.Price < 500000).ToList();
                    break;
                case "2":
                    pro = pro.Where(r => (r.Price >= 500000) && (r.Price <= 1000000)).ToList();
                    break;
                case "3":
                    pro = pro.Where(r => (r.Price >= 1000000) && (r.Price <= 2000000)).ToList();
                    break;
                case "4":
                    pro = pro.Where(r => r.Price > 2000000).ToList();
                    break;

                default:
                    break;
            }

            int hien_thi = 8;
            int so_luong_trang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pro.Count) / Convert.ToDouble(hien_thi)));
            int pro_can_skip = (page - 1) * hien_thi;
            ViewBag.Page = page;
            ViewBag.So_luong_trang = so_luong_trang;
            pro = pro.Skip(pro_can_skip).Take(hien_thi).ToList();
            ViewBag.Loai = loai;
            ViewBag.SortPrice = sortPrice;
            ViewBag.Page = page;
            return View(pro);
        }
        [MyAuthenFilter]
        public ActionResult Detail(int id)
        {
            Product pro = db.Products.Where(i => i.Id == id).FirstOrDefault();
            return View(pro);
        }
    }
}
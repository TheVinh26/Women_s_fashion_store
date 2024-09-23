using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDoNu.Models;
using ShopDoNu.Filters;
using System.IO;
namespace ShopDoNu.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class Product_ManagementController : Controller
    {
        // GET: Admin/Product_Management
        ShopDoNuContext db = new ShopDoNuContext();
        public ActionResult Index()
        {
            List<Product> pro = db.Products.ToList();
            return View(pro);
        }
        public ActionResult DeleteProduct(int id)
        {
            Product pro = db.Products.Where(t => t.Id == id).FirstOrDefault();
            ViewBag.Category = db.Categories.FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id, Product pro)
        {
            Product p = db.Products.Where(t => t.Id == id).FirstOrDefault();
            ViewBag.Category = db.Categories.FirstOrDefault();
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Product_Management", new { area = "Admin" });
        }
        //====================================================================================================
        public ActionResult AddProduct()
        {
            ViewBag.Category = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase imageFile)
        {
            if(ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Img", "File size must be less than 2MB");
                        return View();
                    }

                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Img", "Only jpg or png image files are accepted.");
                        return View();
                    }

                    product.Img = "";
                    db.Products.Add(product);
                    db.SaveChanges();

                    Product pro = db.Products.ToList().Last();

                    var fileName = pro.Id.ToString() + fileEx; //Create Name img
                    var path = Path.Combine(Server.MapPath("~/Img/All"), fileName); //Create link image 
                    imageFile.SaveAs(path); //Save img
                    pro.Img = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product_Management", new { area = "Admin" });
                }
                else
                {
                    product.Img = "";
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product_Management", new { area = "Admin" });
                }
            }
            else
            {
                return View();
            }            
        }
        //====================================================================================================
        public ActionResult UpdateProduct(int id)
        {
            Product p = db.Products.Where(t => t.Id == id).FirstOrDefault();
            ViewBag.Name = p.Name;
            ViewBag.Price = p.Price;
            ViewBag.Description = p.Description;
            ViewBag.Img = p.Img;
            ViewBag.Category = db.Categories.ToList();
            return View(p);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("Image", "File size must be less than 2MB.");
                        return View();
                    }

                    var allowEx = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowEx.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Only jpg or png image files are accepted.");
                        return View();
                    }
                    Product pr = db.Products.Where(t => t.Id == product.Id).FirstOrDefault();
                    pr.Name = product.Name;
                    pr.Price = product.Price;
                    pr.Description = product.Description;
                    pr.Img = ""; //Imgage
                    pr.Id = pr.Id;
                    db.SaveChanges();

                    Product pro = db.Products.Where(t => t.Id == product.Id).FirstOrDefault();
                    var fileName = pro.Id.ToString() + fileEx; //Create Name img
                    var path = Path.Combine(Server.MapPath("~/Img/All"), fileName); //Create link image 
                    imageFile.SaveAs(path); //Save img

                    pro.Name = product.Name;
                    pro.Price = product.Price;
                    pro.Description = product.Description;
                    pro.Img = fileName; //Imgage
                    pro.Id = pro.Id;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product_Management", new { area = "Admin" });
                }
                else
                {
                    Product pr = db.Products.Where(t => t.Id == product.Id).FirstOrDefault();
                    pr.Name = product.Name;
                    pr.Price = product.Price;
                    pr.Description = product.Description;
                    pr.Img = ""; //Imgage
                    pr.Id = pr.Id;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product_Management", new { area = "Admin" });
                }
            }
            else
            {
                return View();
            }
        }     

    }
}
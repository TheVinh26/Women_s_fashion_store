using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDoNu.Models;
using ShopDoNu.Identity;
using ShopDoNu.Filters;
using Microsoft.AspNet.Identity;
namespace ShopDoNu.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ShopDoNuContext db = new ShopDoNuContext();
        [MyAuthenFilter]
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            List<Cart> carts = db.Carts.Where(i => i.UserId == currentUserId).ToList();
            return View(carts);
        }
        [HttpPost]
        public ActionResult AddToCart(int ProductId = 0, int quantity = 1)
        {
            string currentUserId = User.Identity.GetUserId();

            //Cart cartItem = db.Carts.SingleOrDefault(c => c.UserId == currentUserId && c.ProductId == ProductId);
            // Tìm kiếm giỏ hàng để kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
            Cart cartItem = db.Carts.FirstOrDefault(cart => cart.ProductId == ProductId && cart.UserId == currentUserId);
            if (cartItem != null)
            {
                // If the product is already in the cart, update the quantity
                cartItem.Quantity += quantity;
            }
            else
            {
                // If the product is not in the cart, add a new cart item
                Cart newCartItem = new Cart
                {
                    ProductId = ProductId,
                    UserId = currentUserId,
                    Quantity = quantity
                };

                db.Carts.Add(newCartItem);
            }
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteCartItem(int cartId)
        {
            // Retrieve the cart item
            var cartItem = db.Carts.Find(cartId);

            if (cartItem != null)
            {
                // Remove the cart item from the database
                db.Carts.Remove(cartItem);
                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public JsonResult UpdateQuantity(int cartId, int quantity)
        {
            try
            {
                // Kiểm tra quyền truy cập ở đây nếu cần

                // Lấy giỏ hàng từ cơ sở dữ liệu dựa trên cartId
                var cartItem = db.Carts.Find(cartId);

                if (cartItem != null)
                {
                    // Cập nhật số lượng
                    cartItem.Quantity = quantity;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    return Json(new { success = true, message = "Quantity updated successfully." });
                }

                return Json(new { success = false, message = "Failed to update quantity. Cart item not found." });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ở đây nếu cần
                return Json(new { success = false, message = "An error occurred while updating quantity." });
            }
        }

    }
}
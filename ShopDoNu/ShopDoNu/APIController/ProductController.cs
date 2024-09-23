using ShopDoNu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
namespace ShopDoNu.APIController
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public List<Product> Get()
        {
            ShopDoNuContext db = new ShopDoNuContext();
            db.Configuration.ProxyCreationEnabled = false;
            List<Product> pro = db.Products.ToList();
            return pro;
        }
    }
}

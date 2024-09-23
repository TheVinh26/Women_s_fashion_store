using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopDoNu.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
namespace ShopDoNu.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        //public virtual AppUser User { get; set; }
        public virtual Product Product { get; set; }

    }
}
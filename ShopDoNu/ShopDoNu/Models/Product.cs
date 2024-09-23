using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShopDoNu.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name product cannot be empty!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price cannot be empty!")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string Img { get; set; }

        [Required(ErrorMessage = "Category cannot be empty!")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
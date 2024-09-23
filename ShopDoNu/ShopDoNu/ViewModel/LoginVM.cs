using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace ShopDoNu.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Name cannot be blank!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not be blank!")]
        public string Password { get; set; }
    }
}
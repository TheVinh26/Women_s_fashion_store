using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShopDoNu.Identity
{
    public class AppUser: IdentityUser
    {
        [Required(ErrorMessage = "Email cannot be blank!")]
        [EmailAddress(ErrorMessage = "Invalid email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address cannot be empty!")]
        public string Address { get; set; }
    }
}
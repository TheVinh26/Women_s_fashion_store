using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShopDoNu.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name cannot be blank!")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Invalid username. Only unsigned letters and numbers are accepted.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password can not be blank!")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Invalid password. Only unsigned letters and numbers are accepted.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password can not be blank!")]        
        [Compare("Password", ErrorMessage = "Password is different!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email cannot be blank!")]
        [EmailAddress(ErrorMessage = "Invalid email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number can not be left blank!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address cannot be empty!")]
        public string Address { get; set; }
    }
}
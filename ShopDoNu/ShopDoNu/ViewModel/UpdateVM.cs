using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShopDoNu.ViewModel
{
    public class UpdateVM
    {
        [Required(ErrorMessage = "Tên không được để trống!" )]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "SĐT không được để trống!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address không được để trống!")]
        public string Address { get; set; }
    }
}
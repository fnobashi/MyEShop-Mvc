using Microsoft.AspNetCore.Mvc;
using MyEShop.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace MyEShop.Models.Entities.Users
{
    public class Address:BaseEntity
    {
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AddressText { get; set; } = string.Empty;
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "کد پستی نمیتواند بیشتر از 10 رقم باشد")]
        [RegularExpression(Regex.PostalCode)]
        public string PostalCode { get; set; } = string.Empty; 

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

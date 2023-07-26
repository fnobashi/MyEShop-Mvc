using Microsoft.CodeAnalysis.Options;
using MyEShop.Constants;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Areas.Admin.Models.ViewModels.Users
{
    public class UserAddressViewModel
    {
        public long? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AddressText { get; set; } = string.Empty;
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "کد پستی نمیتواند بیشتر از 10 رقم باشد")]
        [RegularExpression(Regex.PostalCode)]
        public string PostalCode { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
    }
}

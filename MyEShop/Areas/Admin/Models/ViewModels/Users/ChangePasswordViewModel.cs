using System.ComponentModel.DataAnnotations;

namespace MyEShop.Areas.Admin.Models.ViewModels.Users
{
    public class ChangePasswordViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبور با تکرار آن برابر نیست")]
        public string RePassword { get; set; }

    }
}

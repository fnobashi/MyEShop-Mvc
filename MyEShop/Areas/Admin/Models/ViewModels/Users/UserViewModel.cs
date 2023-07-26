using MyEShop.Constants;
using MyEShop.Contracts.Repositories;
using MyEShop.Models.Entities.Users;
using MyEShop.Utilities;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Areas.Admin.Models.ViewModels.Users
{
	public class UserViewModel
	{
        public Guid? Id { get; set; }
        [Display(Name = "نام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Name { get; set; }

		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Family { get; set; }

		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[EmailAddress(ErrorMessage = "لطفا ایمیل را صحیح وارد کنید")]
		[UniqueEmail(ErrorMessage ="ایمیل تکراری است")]
		public string Email { get; set; }

		[Display(Name = "تلفن همراه")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[RegularExpression(Regex.PhoneNumber, ErrorMessage = "شماره وارد شده صحیح نیست")]
		[UniquePhone(ErrorMessage = "شماره تلفن وارد شده از قبل موجود است")]
		public string Phone { get; set; }

		[Display(Name="کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name="تکرار کلمه عبور")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password) , ErrorMessage ="کلمه عبور با تکرار آن برابر نیست")]
        public string RePassword { get; set; }
        public DateTime? CreateDate { get; set; }

        [Display(Name = "مدیر")]
        public bool IsAdmin { get; set; } = false;
        [Display(Name = "اپراتور سایت")]
        public bool IsOperator { get; set; } = false;
        [Display(Name = "کاربر معمولی")]
        public bool IsNormalUser { get; set; } = true;

    }
}

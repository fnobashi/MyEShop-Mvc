using MyEShop.Models.Entities.Users;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Areas.Admin.Models.ViewModels.Users
{
	public class UserViewModel
	{
		[Display(Name = "نام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Name { get; set; }

		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Family { get; set; }

		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[EmailAddress(ErrorMessage = "لطفا ایمیل را صحیح وارد کنید")]
		public string Email { get; set; }

		[Display(Name = "آدرس")]
		public string? Address { get; set; }

		[Display(Name = "تلفن همراه")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[RegularExpression(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$", ErrorMessage = "شماره وارد شده صحیح نیست")]
		public string Phone { get; set; }

		[Display(Name = "کد پستی")]
		[DataType(DataType.PostalCode)]
		[RegularExpression(@"\b(?!(\d)\1{3})[13-9]{4}[1346-9][013-9]{5}\b", ErrorMessage = "کد پستی وارد شده صحیح نمی باشد")]
		public string? PostalCode { get; set; }

		[Display(Name="کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name="تکرار کلمه عبور")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password) , ErrorMessage ="کلمه عبور با تکرار آن برابر نیست")]
        public string RePassword { get; set; }
    }
}

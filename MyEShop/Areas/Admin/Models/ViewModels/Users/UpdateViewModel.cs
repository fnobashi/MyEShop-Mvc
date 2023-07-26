using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MyEShop.Constants;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Areas.Admin.Models.ViewModels.Users
{
	public class UpdateViewModel
	{
		public Guid Id { get; set; }
		[Display(Name = "نام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Name { get; set; }

		[Display(Name = "نام خانوادگی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Family { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "ایمیل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[EmailAddress(ErrorMessage = "لطفا ایمیل را صحیح وارد کنید")]
		public string Email { get; set; }

		[Display(Name = "تلفن همراه")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[RegularExpression(Regex.PhoneNumber, ErrorMessage = "شماره وارد شده صحیح نیست")]
		public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOperator { get; set; }
        public bool IsNormalUser { get; set; }

    }
}

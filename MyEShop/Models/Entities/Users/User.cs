using Microsoft.AspNetCore.SignalR.Protocol;
using MyEShop.Constants;
using MyEShop.Models.Entities.Product;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEShop.Models.Entities.Users
{
    public class User :BaseEntity<Guid>
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
        public string ImageName { get; set; } = string.Empty;
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(Regex.PhoneNumber, ErrorMessage = "شماره وارد شده صحیح نیست")]
        public string Phone { get; set; }
        public string Password { get; set; }

        public List<UserInRole> Roles { get; set; }
        public List<Address> Addresses { get; set; }

        public ICollection<ProductQuestionAndAnswer>? Questions { get; set; }
        public ICollection<ProductComment>? Comments { get; set; }

    }
}

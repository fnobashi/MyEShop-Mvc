using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyEShop.Models.Entities.Category
{
    public class Category:BaseEntity
    {
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Name { get; set; } 
        public List<Category>? Children { get; set; }
        [Display(Name="گروه پدر")]
		public long? ParentId { get; set; }
        [Display(Name = "گروه پدر")]
        public Category? Parent { get; set; }

        public List<Category>? AllCategories { get; set; }
        public bool IsDuplicate { get; set; }
        public ICollection<MyEShop.Models.Entities.Product.Product>? Product { get; set; }
    }
}

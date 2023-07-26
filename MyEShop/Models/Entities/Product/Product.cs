using Microsoft.CodeAnalysis;
using MyEShop.Models.Entities.Users;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEShop.Models.Entities.Product
{
    public class Product:BaseEntity
    {
        [MaxLength(200 , ErrorMessage ="نمیتوانید بیشتر از 200 کاراکتر استفاده کنید")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "نمیتوانید بیشتر از 200 کاراکتر استفاده کنید")]
        public string LatinName { get; set; }
        [Display(Name = "شرح محصول")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Description { get; set; }
        public int Inventory { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductAttribute>? ProductAttributes { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductQuestionAndAnswer>? ProductQuestions { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public MyEShop.Models.Entities.Category.Category? Category { get; set; }
    }

    public class ProductImage:BaseEntity
    {
        [MaxLength(200)]
        public string ImageName { get; set; }
        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }

    public class ProductAttribute:BaseEntity
    {
        [MaxLength(150)]
        public string Key { get; set; }
        [MaxLength(150)]
        public string Value { get; set; }
        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }

    public class ProductComment:BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [MaxLength(300 , ErrorMessage ="عنوان نمی تواند بیشتر از 300 کاراکتر داشته باشد")]
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public ICollection<CommentWeakPoints>? WeakPoints { get; set; }
        public ICollection<CommentPositivePoints>? PositivePoints { get; set; }
        public int? CommentLikes { get; set; }
        public int? CommentDislikes { get; set; }
        public bool IsRecommended { get; set; }
        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

    }

    public class CommentWeakPoints:BaseEntity
    {
        [MaxLength(100 ,ErrorMessage = "نمیتوانید بیشتر از 100 کاراکتر وارد نمایید")]
        public string Text { get; set; }
        public long ProductCommentId { get; set; }
        [ForeignKey(nameof(ProductCommentId))]
        public ProductComment ProductComment { get; set; }
    }
    public class CommentPositivePoints:BaseEntity
    {
        [MaxLength(100, ErrorMessage = "نمیتوانید بیشتر از 100 کاراکتر وارد نمایید")]
        public string Text { get; set; }
        public long ProductCommentId { get; set; }
        [ForeignKey(nameof(ProductCommentId))]
        public ProductComment ProductComment { get; set; }
    }

    public class ProductQuestionAndAnswer :BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public List<ProductQuestionAndAnswer> Answers { get; set; }
        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace MyEShop.Models.Entities
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
    }

    public class BaseEntity : BaseEntity<long>
    {
        
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MyEShop.Models.Entities.Users
{
	public class UserInRole:BaseEntity
	{
        public Guid UserId { get; set; }
		[ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long RoleId { get; set; }
		[ForeignKey(nameof(RoleId))]
        public UserRoles Role { get; set; }

    }
}

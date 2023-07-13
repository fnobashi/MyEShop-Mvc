using Microsoft.EntityFrameworkCore;
using MyEShop.Models.Entities.Users;

namespace MyEShop.Models.Context
{
    public class ShopDbContext :DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }


    }
}

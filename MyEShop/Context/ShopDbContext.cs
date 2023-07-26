using Microsoft.EntityFrameworkCore;
using MyEShop.Constants;
using MyEShop.Models.Entities.Category;
using MyEShop.Models.Entities.Product;
using MyEShop.Models.Entities.Users;

namespace MyEShop.Context
{
	public class ShopDbContext : DbContext
	{
		public ShopDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<User> Users { get; set; }
		public DbSet<UserRoles> Roles { get; set; }
		public DbSet<UserInRole> UserInRole { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Category> Categories { get; set; }


		// product 
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductComment> ProductComments { get; set; }
		public DbSet<CommentPositivePoints> CommentPositivePoints { get; set; }
        public DbSet<CommentWeakPoints> CommentWeakPoints { get; set; }
		public DbSet<ProductAttribute> ProductAttributes { get; set; }
		public DbSet<ProductQuestionAndAnswer> ProductQuestionAndAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
			modelBuilder.Entity<UserRoles>().HasData(new UserRoles { Id = 1, CreateDate = DateTime.Now, RoleName = RoleName.Admin });
			modelBuilder.Entity<UserRoles>().HasData(new UserRoles { Id = 2, CreateDate = DateTime.Now, RoleName = RoleName.Operator });
			modelBuilder.Entity<UserRoles>().HasData(new UserRoles { Id = 3, CreateDate = DateTime.Now, RoleName = RoleName.Customer });
			modelBuilder.Entity<Address>()
		   .HasQueryFilter(a => !a.IsDeleted)
		   .HasOne(a => a.User)
		   .WithMany(u => u.Addresses)
		   .HasForeignKey(a => a.UserId);

			modelBuilder.Entity<Category>()
				.HasQueryFilter(c => !c.IsDeleted)
				.HasMany(c => c.Children)
				.WithOne(c => c.Parent)
				.HasForeignKey(c => c.ParentId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ProductQuestionAndAnswer>()
				.HasQueryFilter(q => !q.IsDeleted)
				.HasOne(q => q.User)
				.WithMany(q => q.Questions)
				.HasForeignKey(q => q.UserId);

			modelBuilder.Entity<ProductComment>()
				.HasQueryFilter(c => c!.IsDeleted)
				.HasOne(c => c.User)
				.WithMany(c => c.Comments)
				.HasForeignKey(c => c.UserId);

			modelBuilder.Entity<Product>()
				.HasQueryFilter(p => !p.IsDeleted)
				.HasOne(p => p.Category)
				.WithMany(p => p.Product)
				.HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<ProductImage>()
				.HasQueryFilter(p => !p.IsDeleted)
				.HasOne(p => p.Product)
				.WithMany(p => p.ProductImages)
				.HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<CommentPositivePoints>()
                .HasQueryFilter(p => !p.IsDeleted)
				.HasOne(p => p.ProductComment)
				.WithMany(p => p.PositivePoints)
				.HasForeignKey(p => p.ProductCommentId);

            modelBuilder.Entity<CommentWeakPoints> ()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasOne(p => p.ProductComment)
                .WithMany(p => p.WeakPoints)
                .HasForeignKey(p => p.ProductCommentId);

            modelBuilder.Entity<ProductAttribute>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(p => p.ProductId);

            base.OnModelCreating(modelBuilder);
		}
	}
}

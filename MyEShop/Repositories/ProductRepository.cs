using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;
using MyEShop.Models.Entities.Users;
using NuGet.Protocol;

namespace MyEShop.Repositories
{
    public class ProductRepository : GenericRepository<Product, long>, IProductRepository
    {
        private readonly ShopDbContext context;

        public ProductRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
		public override Product GetById(long id)
		{
			return context.Products.Where(u => u.Id == id).Include(p => p.ProductImages).Single();
		}
		public async override Task<Product> GetByIdAsync(long id)
		{
			var result = new Product();
			await Task.Run(() =>
			{
				result = context.Products.Where(u => u.Id == id).Include(p =>p.ProductImages).Single();
			});
			return result;
		}
		public override IQueryable<Product> GetAll(SortBy sort)
        {

            switch (sort)
            {
                case SortBy.Ascending:
                    return context.Products.OrderBy(o => o.Name);
                case SortBy.Descending:
                    return context.Products.OrderByDescending(o => o.Name);
                case SortBy.CreateDateAscending:
                    return context.Products.OrderBy(o => o.CreateDate);
                case SortBy.CreateDateDescending:
                    return context.Products.OrderByDescending(o => o.CreateDate);
                case SortBy.ModifiedDateAscending:
                    return context.Products.OrderBy(o => o.ModifiedDate);
                case SortBy.ModifiedDateDescending:
                    return context.Products.OrderByDescending(o => o.ModifiedDate);
                default:
                    return context.Products.OrderBy(o => o.Name);
            }
        }
    }
}

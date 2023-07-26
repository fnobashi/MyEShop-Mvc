using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;

namespace MyEShop.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage, long>, IProductImageRepository
    {
        private readonly ShopDbContext context;

        public ProductImageRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<ProductImage> GetAll(SortBy sort)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ProductImage>> GetImagesByProductIdAsync(long productId)
            => await context.ProductImages.Where(p => p.ProductId == productId).ToListAsync();
        
    }


}

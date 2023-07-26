using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;

namespace MyEShop.Repositories
{
    public class ProductAttributeRepository : GenericRepository<ProductAttribute, long>, IProductAttributeRepository
    {
        private readonly ShopDbContext context;

        public ProductAttributeRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<ProductAttribute> GetAll(SortBy sort)
        {
            throw new NotImplementedException();
        }
    }


}

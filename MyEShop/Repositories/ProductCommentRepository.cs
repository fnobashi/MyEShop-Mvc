using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;

namespace MyEShop.Repositories
{
    public class ProductCommentRepository : GenericRepository<ProductComment, long>, IProductCommentRepository
    {
        private readonly ShopDbContext context;

        public ProductCommentRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<ProductComment> GetAll(SortBy sort)
        {
            throw new NotImplementedException();
        }
    }


}

using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;

namespace MyEShop.Repositories
{
    public class ProductQuestionAndAnswerRepository : GenericRepository<ProductQuestionAndAnswer, long>, IProductQuestionAndAnswerRepository
    {
        private readonly ShopDbContext context;

        public ProductQuestionAndAnswerRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<ProductQuestionAndAnswer> GetAll(SortBy sort)
        {
            throw new NotImplementedException();
        }
    }


}

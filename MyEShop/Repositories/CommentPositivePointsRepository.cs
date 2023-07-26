using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;

namespace MyEShop.Repositories
{
    public class CommentPositivePointsRepository : GenericRepository<CommentPositivePoints, long>, ICommentPositivePointsRepository
    {
        private readonly ShopDbContext context;

        public CommentPositivePointsRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<CommentPositivePoints> GetAll(SortBy sort)
        {
            throw new NotImplementedException();
        }
    }


}

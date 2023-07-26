using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Product;

namespace MyEShop.Repositories
{
    public class CommentWeakPointsRepository : GenericRepository<CommentWeakPoints, long>, ICommentWeakPointsRepository
    {
        private readonly ShopDbContext context;

        public CommentWeakPointsRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<CommentWeakPoints> GetAll(SortBy sort)
        {
            throw new NotImplementedException();
        }
    }


}

using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Category;
using MyEShop.Models.Entities.Users;

namespace MyEShop.Repositories
{
    public class CategoryRepository : GenericRepository<Category, long>, ICategoryRepository
    {
        private readonly ShopDbContext context;

        public CategoryRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }
        public override IQueryable<Category> GetAll(SortBy sort)
        {
            switch (sort)
            {
                case SortBy.Ascending:
                    return context.Categories.OrderBy(o => o.Name);
                case SortBy.Descending:
                    return context.Categories.OrderByDescending(o => o.Name);
                case SortBy.CreateDateAscending:
                    return context.Categories.OrderBy(o => o.CreateDate);
                case SortBy.CreateDateDescending:
                    return context.Categories.OrderByDescending(o => o.CreateDate);
                case SortBy.ModifiedDateAscending:
                    return context.Categories.OrderBy(o => o.ModifiedDate);
                case SortBy.ModifiedDateDescending:
                    return context.Categories.OrderByDescending(o => o.ModifiedDate);
                default:
                    return context.Categories.OrderBy(o => o.Name);
            }
        }
        public bool IsExist(long id)
            => context.Categories.Any(c => c.Id == id);
    }
}

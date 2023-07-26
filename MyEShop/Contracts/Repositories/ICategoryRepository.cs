using MyEShop.Models.Entities.Category;

namespace MyEShop.Contracts.Repositories
{
    public interface ICategoryRepository:IBaseRepository<Category ,long> 
    {
        bool IsExist(long id);
    }
}

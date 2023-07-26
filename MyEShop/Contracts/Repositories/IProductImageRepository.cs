using MyEShop.Models.Entities.Product;

namespace MyEShop.Contracts.Repositories
{
    public interface IProductImageRepository:IBaseRepository<ProductImage , long> 
    {
        Task<ICollection<ProductImage>> GetImagesByProductIdAsync(long productId);
    }
}

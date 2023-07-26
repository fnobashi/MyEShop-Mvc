using MyEShop.Models.Entities.Product;

namespace MyEShop.Contracts.Repositories
{
    public interface IProductRepository:IBaseRepository<Product , long>
    {
    }
}

using MyEShop.Models.Entities.Users;

namespace MyEShop.Contracts.Repositories
{
    public interface IUserAddressRepository : IBaseRepository<Address, long>
    {
        IQueryable<Address> GetAddressesByUserId(Guid id);
    }
}
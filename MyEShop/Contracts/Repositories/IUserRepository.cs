using MyEShop.Models.Entities.Users;

namespace MyEShop.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        string GetPassword(Guid id);

        void ChangePassword(Guid id, string password);

        bool IsEmailInUse(string email);

        bool IsPhoneNumberInUse(string phoneNumber);

        bool IsUserExist(Guid id);
    }
}
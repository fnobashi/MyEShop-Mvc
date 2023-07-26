using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Users;
using MyEShop.Utilities;

namespace MyEShop.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        private readonly ShopDbContext dbcontext;
        public UserRepository(ShopDbContext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public override User GetById(Guid id)
            => dbcontext.Users.Where(u => u.Id == id).Include(a => a.Roles).Single();

        public override async Task<User> GetByIdAsync(Guid id)
        {

            var result = new User();
            await Task.Run(() =>
            {
                result = dbcontext.Users.Where(u => u.Id == id).Include(a => a.Roles).Single();
            });
            return result;

        }

        public override void Add(User entity)
        {
            var hashedPassword = Hasher.HashString(entity.Password);
            entity.Password = hashedPassword;
            base.Add(entity);
        }
        public override async Task AddAsync(User entity)
        {
            var hashedPassword = Hasher.HashString(entity.Password);
            entity.Password = hashedPassword;
            await base.AddAsync(entity);
        }

        public string GetPassword(Guid id)
        {
            var password = dbcontext.Users.First(u => u.Id == id).Password;
            if (!string.IsNullOrWhiteSpace(password))
                return password;

            throw new NullReferenceException();
        }

        public void ChangePassword(Guid id, string password)
        {
            var hashedPassword = Hasher.HashString(password);
            var user = dbcontext.Users.First(u => u.Id == id).Password = hashedPassword;
            dbcontext.SaveChanges();
        }

        public bool IsEmailInUse(string email)
            => dbcontext.Users.Any(u => u.Email == email);

        public bool IsPhoneNumberInUse(string phoneNumber)
            => dbcontext.Users.Any(u => u.Phone == phoneNumber);

        public bool IsUserExist(Guid id)
            => dbcontext.Users.Any(u => u.Id == id);

        public override IQueryable<User> GetAll(SortBy sort)
        {
            switch (sort)
            {
                case SortBy.Ascending:
                    return dbcontext.Users.OrderBy(o => o.Family);
                case SortBy.Descending:
                    return dbcontext.Users.OrderByDescending(o => o.Family);
                case SortBy.CreateDateAscending:
                    return dbcontext.Users.OrderBy(o => o.CreateDate);
                case SortBy.CreateDateDescending:
                    return dbcontext.Users.OrderByDescending(o => o.CreateDate);
                case SortBy.ModifiedDateAscending:
                    return dbcontext.Users.OrderBy(o => o.ModifiedDate);
                case SortBy.ModifiedDateDescending:
                    return dbcontext.Users.OrderByDescending(o => o.ModifiedDate);
                default:
                    return dbcontext.Users.OrderBy(o => o.Family);
            }
        }
    }
}

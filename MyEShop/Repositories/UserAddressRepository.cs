using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Users;
using System.Globalization;
using System.Security.Cryptography.Xml;

namespace MyEShop.Repositories
{
    public class UserAddressRepository : GenericRepository<Address, long>, IUserAddressRepository
    {
        private readonly ShopDbContext context;

        public UserAddressRepository(ShopDbContext context) : base(context)
        {
            this.context = context;
        }

        public override Address GetById(long id)
            => context.Addresses.Where(a => a.Id == id).Include(a => a.User).Single();

        public override async Task<Address> GetByIdAsync(long id)
        {
            Address result = new Address();
            await Task.Run(() =>
           {
               result = context.Addresses.Where(a => a.Id == id).Include(a => a.User).Single();
           });
            return result;
        }

        public IQueryable<Address> GetAddressesByUserId(Guid id)
            => context.Addresses.Where(a => a.UserId == id).Include(a => a.User);

        public override IQueryable<Address> GetAll(SortBy sort)
        {
            switch (sort)
            {
                case SortBy.Ascending:
                    return context.Addresses.OrderBy(o => o.AddressText);
                case SortBy.Descending:
                    return context.Addresses.OrderByDescending(o => o.AddressText);
                case SortBy.CreateDateAscending:
                    return context.Addresses.OrderBy(o => o.CreateDate);
                case SortBy.CreateDateDescending:
                    return context.Addresses.OrderByDescending(o => o.CreateDate);
                case SortBy.ModifiedDateAscending:
                    return context.Addresses.OrderBy(o => o.ModifiedDate);
                case SortBy.ModifiedDateDescending:
                    return context.Addresses.OrderByDescending(o => o.ModifiedDate);
                default:
                    return context.Addresses.OrderBy(o => o.AddressText);
            }
        }

    
    }
}

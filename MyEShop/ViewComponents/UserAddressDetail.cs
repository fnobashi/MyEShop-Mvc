using Microsoft.AspNetCore.Mvc;
using MyEShop.Contracts.Repositories;

namespace MyEShop.ViewComponents
{
    public class UserAddressDetail:ViewComponent
    {
        private readonly IUserAddressRepository userAddressRepository;

        public UserAddressDetail(IUserAddressRepository userAddressRepository)
        {
            this.userAddressRepository = userAddressRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            var user = await userAddressRepository.GetByIdAsync(id);
            return View("UserAddressDetail" , user);
        }
    }
}

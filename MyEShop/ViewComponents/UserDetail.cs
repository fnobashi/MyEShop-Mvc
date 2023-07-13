using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyEShop.Areas.Admin.Models.ViewModels.Users;
using MyEShop.Models.Context;

namespace MyEShop.ViewComponents
{
    public class UserDetail:ViewComponent
    {
        private readonly ShopDbContext context;

        public UserDetail(ShopDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            var user = await context.Users.FindAsync(id);

            return View("UserDetail" , user);
        }
    }
}

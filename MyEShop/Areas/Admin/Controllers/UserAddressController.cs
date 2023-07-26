using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MyEShop.Areas.Admin.Models.ViewModels.Users;
using MyEShop.Constants;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Users;
using MyEShop.Repositories;
using MyEShop.Utilities;

namespace MyEShop.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class UserAddressController : Controller
    {
        private readonly IUserAddressRepository userAddressRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserAddressController> logger;

        public UserAddressController(IUserAddressRepository userAddressRepository, IUserRepository userRepository, ILogger<UserAddressController> logger)
        {
            this.userAddressRepository = userAddressRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }
        [Route("Admin/UserAddress")]
        [HttpGet]
        public async Task<IActionResult> Index(PaginationModel? pagination, SortBy sortBy)
        {
            int pageSize;

            if (pagination?.PageSize > 0)
            {
                Response.Cookies.Append(CookieName.PageSize, pagination.PageSize.ToString());
                pageSize = pagination.PageSize;
            }
            else
            {
                pageSize = int.Parse(Request.Cookies[CookieName.PageSize] ?? "5");
            }
            var page = pagination?.Page ?? 1;

            int totalPages;
            var res = userAddressRepository.GetAll(sortBy).ToPaged(page, pageSize, out totalPages);

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(await res.Include(a => a.User).ToListAsync());
        }

        [Route("Admin/UserAddress/{id}")]
        public async Task<IActionResult> Index(Guid id, int page = 1, int pageSize = 5)
        {

            bool IsUserExist = userRepository.IsUserExist(id);
            if (!IsUserExist)
                return NotFound("کاربری با این مشخصات یافت نشد");

            int totalPages;
            var result = await userAddressRepository.GetAddressesByUserId(id).ToPaged(page, pageSize, out totalPages).ToListAsync();
            if (result == null) return NotFound();
            ViewBag.UserId = id;
            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(result);
        }
        #region Add Address 
        [HttpGet]
        [Route("Admin/UserAddress/Add/{userId}")]
        public IActionResult Add(Guid userId)
        {
            bool IsUserExist = userRepository.IsUserExist(userId);
            if (!IsUserExist)
                return NotFound();

            var model = new UserAddressViewModel()
            {
                UserId = userId
            };
            return View(model);
        }

        [Route("Admin/UserAddress/Add/{userId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.UserId == null)
                return NotFound("کاربر یافت نشد");

            var address = new Address
            {
                UserId = model.UserId.Value,
                AddressText = model.AddressText,
                PostalCode = model.PostalCode
            };

            await userAddressRepository.AddAsync(address);
            return RedirectToAction("Index", new { id = model.UserId.Value });
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userAddressRepository.GetByIdAsync(long.Parse(id));
            if (user == null) return NotFound();
            return ViewComponent("UserAddressDetail", new { id = long.Parse(id) });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await userAddressRepository.DeleteAsync(long.Parse(id));
            return Ok();
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(long id)
        {
            var res = await userAddressRepository.GetByIdAsync(id);
            if (res == null) return NotFound();
            return ViewComponent("UserAddressDetail", new { id = id });
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(long id)
        {

            var res = await userAddressRepository.GetByIdAsync(id);

            if (res == null) return NotFound();
            var model = new UserAddressViewModel()
            {
                Id = res.Id,
                CreatedDate = res.CreateDate,
                AddressText = res.AddressText,
                PostalCode = res.PostalCode,
                UserId = res.UserId
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserAddressViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var address = new Address()
            {
                Id = model.Id.Value,
                CreateDate = model.CreatedDate,
                AddressText = model.AddressText,
                PostalCode = model.PostalCode,
                UserId = model.UserId.Value
            };

            await userAddressRepository.UpdateAsync(address);
                return RedirectToAction("Index");
        }
        #endregion
    }
}

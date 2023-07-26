using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEShop.Areas.Admin.Models.ViewModels.Users;
using MyEShop.Constants;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Users;
using MyEShop.Utilities;
using System.Net;
using System.Net.WebSockets;

namespace MyEShop.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<UsersController> logger;
        public UsersController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, ILogger<UsersController> logger)
        {
            this.userRepository = userRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }

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
            var res = userRepository.GetAll(sortBy).ToPaged(page, pageSize, out totalPages);

            var model = await res.Select(u => new UsersListViewModel()
            {
                UserId = u.Id,
                FullName = u.Name + " " + u.Family,
                Email = u.Email,
                PhoneNumber = u.Phone
            }).ToListAsync();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(model);

        }

        #region addUser 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserViewModel user, IFormFile? userImg)
        {
            if (!ModelState.IsValid)
                return View(user);

            var newUser = new User()
            {
                Name = user.Name,
                Family = user.Family,
                Email = user.Email,
                Phone = user.Phone,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                Password = user.Password,

            };

            if (userImg == null)
                newUser.ImageName = "Avatar.jpg";
            else
            {

                string path = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Account");
                var uploadresult = await ImageUploader.UploadAsync(userImg, path);
                if (!uploadresult.IsSuccess && uploadresult.IsValidExtension)
                {
                    newUser.ImageName = "Avatar.jpg";
                    logger.LogError("Image wasnt uploaded");
                }
                else
                {
                    switch (uploadresult.IsValidExtension)
                    {
                        case true:
                            {
                                newUser.ImageName = uploadresult.FileName;
                                break;
                            }
                        case false:
                            {
                                newUser.ImageName = "Avatar.jpg";
                                ModelState.AddModelError(string.Empty, "فرمت فایل پشتیبانی نمیشود");
                                return View(user);
                            }
                    }
                }
            }

            var userRoles = new List<UserInRole>();
            if (user.IsAdmin)
                userRoles.Add(new UserInRole { UserId = newUser.Id, RoleId = 1, CreateDate = DateTime.Now });
            if (user.IsOperator)
                userRoles.Add(new UserInRole { UserId = newUser.Id, RoleId = 2, CreateDate = DateTime.Now });
            if (user.IsNormalUser)
                userRoles.Add(new UserInRole { UserId = newUser.Id, RoleId = 3, CreateDate = DateTime.Now });

            newUser.Roles = userRoles;
            await userRepository.AddAsync(newUser);
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return ViewComponent("UserDetail", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await userRepository.DeleteAsync(Guid.Parse(id));
            return NoContent();
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(Guid id)
        {
            var res = await userRepository.GetByIdAsync(id);
            if (res == null) return NotFound();
            return ViewComponent("UserDetail", new { id = id });
        }
        #endregion

        #region EditUser 
        public async Task<IActionResult> Edit(Guid id)
        {

            var res = await userRepository.GetByIdAsync(id);
            if (res == null) return NotFound();
            var model = new UpdateViewModel()
            {
                Id = res.Id,
                Name = res.Name,
                Family = res.Family,
                Email = res.Email,
                Phone = res.Phone,
                IsAdmin = res.Roles.Any(r => r.RoleId == 1),
                IsOperator = res.Roles.Any(r => r.RoleId == 2),
                IsNormalUser = res.Roles.Any(r => r.RoleId == 3),
                ImageName = res.ImageName
            };

            TempData["Password"] = res.Password;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateViewModel model, IFormFile? userImg)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userRepository.GetByIdAsync(model.Id);
            if (userImg != null)
            {

                string path = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Account");
                System.IO.File.Delete(Path.Combine(path, user.ImageName));
                var uploadResult = await ImageUploader.UploadAsync(userImg, path);
                if (uploadResult.IsSuccess)
                    user.ImageName = uploadResult.FileName;
                else
                    logger.LogError("Image was not Uploaded");
            }

            user.Name = model.Name;
            user.Family = model.Family;
            user.Email = model.Email;
            user.Phone = model.Phone;
            var userRoles = new List<UserInRole>();
            if (model.IsAdmin)
                userRoles.Add(new UserInRole { UserId = user.Id, RoleId = 1, CreateDate = DateTime.Now });
            if (model.IsOperator)
                userRoles.Add(new UserInRole { UserId = user.Id, RoleId = 2, CreateDate = DateTime.Now });
            if (model.IsNormalUser)
                userRoles.Add(new UserInRole { UserId = user.Id, RoleId = 3, CreateDate = DateTime.Now });
            user.Roles = userRoles;
            await userRepository.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion

        [HttpGet]
        public IActionResult ChangePassword(Guid id)
        {
            return View();
        }
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            userRepository.ChangePassword(model.Id, model.Password);
            return RedirectToAction("index");

        }

    }
}

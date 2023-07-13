using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEShop.Areas.Admin.Models.ViewModels.Users;
using MyEShop.Models.Context;
using MyEShop.Models.Entities.Users;
using MyEShop.Utilities;

namespace MyEShop.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class UsersController : Controller
    {
        private readonly ShopDbContext _context;

        public UsersController(ShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            int totalPages;
            var users = _context.Users.ToPaged(page  , pageSize ,  out totalPages ).Select(u => new UsersListViewModel
            {
                UserId = u.Id,
                FullName = u.Name + " " + u.Family,
                Email = u.Email,
                PhoneNumber = u.Phone

            }).ToList();

			ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(users);
        }

        #region addUser 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var newUser = new User()
            {
                Name = user.Name,
                Family = user.Family,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                IsDeleted = false , 
                CreateDate = DateTime.Now  , 
                PostalCode = user.PostalCode  , 
                Password  = user.Password  
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound();
                return ViewComponent("UserDetail", new { id = id });
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            return View();
        }
        #endregion 

        #region EditUser 
        public IActionResult Edit(long id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return NotFound();
                return Json(user);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            _context.Users.Update(user);
            _context.SaveChanges();
            return View("Index");
        }
        #endregion

        #region Detail
        public IActionResult Detail(long id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return NotFound();
                return ViewComponent("UserDetail", new { id = id });

            }
            catch
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}

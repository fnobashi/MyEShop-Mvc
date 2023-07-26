using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEShop.Areas.Admin.Models.ViewModels.Users;
using MyEShop.Constants;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Category;
using MyEShop.Models.Entities.Users;
using MyEShop.Repositories;
using MyEShop.Utilities;
using NuGet.Protocol;
using System.Text.RegularExpressions;

namespace MyEShop.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [Route("Admin/Category")]
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
            var res = await categoryRepository.GetAll(sortBy).ToPaged(page, pageSize, out totalPages).ToListAsync();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            MarkDuplicates(res);
            return View(res);
        }

        private void MarkDuplicates(List<Category> categories)
        {
            var categoryIds = new HashSet<long>();
            foreach (var category in categories)
            {
                if (categoryIds.Contains(category.Id))
                {
                    category.IsDuplicate = true;
                }
                else
                {
                    categoryIds.Add(category.Id);
                }
                if (category.Children != null)
                {
                    MarkDuplicates(category.Children);
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new Category
            {
                AllCategories = await categoryRepository.GetAll().ToListAsync() 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreateDate = DateTime.UtcNow;
                category.IsDeleted = false;

                if (category.ParentId.HasValue)
                {
                    var parentCategory = await categoryRepository.GetByIdAsync(category.ParentId.Value);
                    parentCategory.Children ??= new List<Category>();
                    parentCategory.Children.Add(category);
                }

                await categoryRepository.AddAsync(category);
                return RedirectToAction("Index");
            }
            category.AllCategories = await categoryRepository.GetAll().ToListAsync();
            return View(category);
        }


        public async Task<IActionResult> Edit(long id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.AllCategories = await categoryRepository.GetAll().ToListAsync(); 
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = await categoryRepository.GetByIdAsync(id);
                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    existingCategory.Name = category.Name;
                    existingCategory.ParentId = category.ParentId;

                    if (category.ParentId.HasValue)
                    {
                        var parentCategory = await categoryRepository.GetByIdAsync(category.ParentId.Value);
                        parentCategory.Children ??= new List<Category>();
                        if (!parentCategory.Children.Any(c => c.Id == existingCategory.Id))
                        {
                            parentCategory.Children.Add(existingCategory);
                        }
                    }

                    await categoryRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }

            category.AllCategories = await categoryRepository.GetAll().ToListAsync();
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

  

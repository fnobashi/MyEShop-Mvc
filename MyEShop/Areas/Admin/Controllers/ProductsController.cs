using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEShop.Constants;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities.Category;
using MyEShop.Models.Entities.Product;
using MyEShop.Repositories;
using MyEShop.Utilities;

namespace MyEShop.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductImageRepository productImageRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.productImageRepository = productImageRepository;
            this.webHostEnvironment = webHostEnvironment;
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
            var res = await productRepository.GetAll(sortBy).Include(p => p.Category).ToPaged(page, pageSize, out totalPages).ToListAsync();

            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var list = new SelectList(await categoryRepository.GetAll().ToListAsync(), nameof(Category.Id), nameof(Category.Name));
            ViewBag.CategoryList = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, ICollection<IFormFile>? productImages)
        {
            if (!ModelState.IsValid) return View(product);
            string imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "Images", "ProductsImage");
            foreach (var item in productImages)
            {
                var result = await ImageUploader.UploadAsync(item, imagePath);
                if (!result.IsValidExtension)
                {
                    ModelState.AddModelError(string.Empty, "فرمت عکس ها پشتیبانی نمیشود");
                    return View(product);
                }
                if (result.IsSuccess)
                {
                    product.ProductImages ??= new List<ProductImage>();
                    product.ProductImages.Add(new ProductImage() { ImageName = result.FileName, CreateDate = DateTime.UtcNow });
                }

            }
            await productRepository.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var list = new SelectList(await categoryRepository.GetAll().ToListAsync(), nameof(Category.Id), nameof(Category.Name));
            ViewBag.CategoryList = list;
            var model = await productRepository.GetByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, ICollection<IFormFile>? productImages)
        {
            if (!ModelState.IsValid) return View(product);
            string imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "Images", "ProductsImage");

            if (productImages.Count > 0)
            {

                foreach (var item in productImages)
                {
                    var result = await ImageUploader.UploadAsync(item, imagePath);
                    if (!result.IsValidExtension)
                    {
                        ModelState.AddModelError(string.Empty, "فرمت عکس ها پشتیبانی نمیشود");
                        return View(product);
                    }
                    if (result.IsSuccess)
                    {
                        product.ProductImages ??= new List<ProductImage>();
                        product.ProductImages.Add(new ProductImage() { ImageName = result.FileName });
                    }
                }

                var oldImages = await productImageRepository.GetImagesByProductIdAsync(product.Id);
                foreach (var item in oldImages)
                {
                    System.IO.File.Delete(Path.Combine(imagePath, item.ImageName));
                }

            }
            await productRepository.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

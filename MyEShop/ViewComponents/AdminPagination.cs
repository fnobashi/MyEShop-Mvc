using Microsoft.AspNetCore.Mvc;

namespace MyEShop.ViewComponents
{
    public class AdminPagination:ViewComponent
    {
        public IViewComponentResult Invoke(int page , int pageSize ,int totalPages)
        {
            ViewBag.TotalPages = totalPages;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View("AdminPagination");
        }
    }
}

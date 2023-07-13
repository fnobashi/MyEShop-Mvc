using Microsoft.AspNetCore.Mvc;
using MyEShop.Utilities;

namespace MyEShop.Areas.Admin.Controllers
{
    [Area(AreaName.Admin)]
    public class HomeController : Controller
    {
        [Route("admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

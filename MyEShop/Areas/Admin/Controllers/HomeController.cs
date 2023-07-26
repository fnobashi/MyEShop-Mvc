using Microsoft.AspNetCore.Mvc;
using MyEShop.Constants;
using System.Text.RegularExpressions;

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


        // this is for test reason
        public IActionResult Error()
        {
            throw new Exception("Exception");
        }
        public IActionResult Nothing()
        {
            return NotFound();
        }

    }
}

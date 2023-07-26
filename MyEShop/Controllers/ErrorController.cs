using Microsoft.AspNetCore.Mvc;

namespace MyEShop.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            throw new ArgumentNullException();
        }
        public IActionResult Index()
        {
            return NotFound(); 
        }

    }
}

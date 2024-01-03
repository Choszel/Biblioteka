using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("basket.html", "/../JavaScript/basket.html");
        }
    }
}

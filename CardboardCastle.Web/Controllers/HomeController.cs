using Microsoft.AspNetCore.Mvc;

namespace CardboardCastle.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return File("index.html", "text/html");
        }
    }
}

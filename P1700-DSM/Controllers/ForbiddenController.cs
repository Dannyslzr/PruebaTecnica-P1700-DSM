using Microsoft.AspNetCore.Mvc;

namespace P1700_DSM.Controllers
{
    public class ForbiddenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

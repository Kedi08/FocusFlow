using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.Controllers
{
    public class AktivitaetenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

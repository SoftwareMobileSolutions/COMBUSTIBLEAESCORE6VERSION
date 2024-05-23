using Microsoft.AspNetCore.Mvc;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class IntroduccionController : Controller
    {
        public IActionResult Introduccion()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace NBD3.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

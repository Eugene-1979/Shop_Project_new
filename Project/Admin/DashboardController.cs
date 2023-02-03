using Microsoft.AspNetCore.Mvc;

namespace Shop_Project.Admin
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

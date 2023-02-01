using Microsoft.AspNetCore.Mvc;

namespace Shop_Project.Controllers
    {
    public class DashboardController : Controller
        {
        public IActionResult Index()
            {
            return View();
            }
        }
    }

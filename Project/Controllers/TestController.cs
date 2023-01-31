using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Project.Db;
using Shop_Project.Repository;

namespace Shop_Project.Controllers
    {
    public class TestController : Controller
        {
        ITest _testClass;
        AppDbContent _context;
        public TestController(ITest testClass ,AppDbContent context)
            {
            _context = context;
            _testClass = testClass;
            }



  /*      [Authorize(Roles = "admin")]*/
        /*       [Route="i"]*/
        public IActionResult Index()
            {
            if(!User.Identity.IsAuthenticated || !User.IsInRole("Admin")) return RedirectToAction("Test2");
            return Ok("qqq") ;
            }
        [Route("Test1")]
        public IActionResult Test1()
            {
            return _testClass.ReturnFalse() ? Ok("qqq") : NotFound("qwe");
            }

        [Route("Test2")]
        public IActionResult Test2()
            {
            return Ok(_context.Orders.Count());
            }

        }
    }

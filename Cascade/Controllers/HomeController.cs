using Microsoft.AspNetCore.Mvc;

namespace Cascade.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(1);
        }
    }
}

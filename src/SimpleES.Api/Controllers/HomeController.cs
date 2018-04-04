using Microsoft.AspNetCore.Mvc;

namespace SimpleES.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
            => Content("Welcome to SimpleES API.");
    }
}
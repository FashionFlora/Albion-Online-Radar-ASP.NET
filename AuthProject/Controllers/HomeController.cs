using AuthProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PacketCaptureServer;

namespace AuthProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

         

            ViewData["username"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewData["daysLeft"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return View();
        }
        
        public IActionResult Raw()
        {
            ViewData["username"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewData["daysLeft"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return View ();
        }

		public IActionResult Settings()
		{
            ViewData["username"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewData["daysLeft"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return View();
		}


        public IActionResult IgnoreList()
        {
            ViewData["username"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewData["daysLeft"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return View();
        }

        public IActionResult Chests()
		{
            ViewData["username"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewData["daysLeft"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return View();
		}
		public IActionResult Privacy()
        {
            ViewData["username"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewData["daysLeft"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            return View();
        }

        PacketHub packetHub = new PacketHub();


        public IActionResult Drawing() { 


         

            return View();
        }


        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using LogDNA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogDNA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _homeContext; // Fərqli ad istifadə edin

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _homeContext = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // İstifadə edin _homeContext
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

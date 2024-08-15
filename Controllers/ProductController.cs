using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; // DbSet üçün ToListAsync metodunu təmin edir
using LogDNA.Data;
using LogDNA.Models;

namespace LogDNA.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Yeni məhsul əlavə edildi: {ProductName}, Qiymət: {ProductPrice}", product.Name, product.Price);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Index
        public async Task<IActionResult> Index()
        {
            // Bu sətirdə ToListAsync istifadə edirik
            var products = await _context.Products.ToListAsync();
            return View(products);
        }
    }
}

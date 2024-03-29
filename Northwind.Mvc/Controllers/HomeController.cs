using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Models;
using Packt.Shared; // NortwindContext
using System.Diagnostics;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _db;

        public HomeController(ILogger<HomeController> logger, NorthwindContext db)
        {
            _logger = logger;
            _db = db;
        }

        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel
            (
            VisitorCount: new Random().Next(1, 1001),
            Categories: await _db.Categories.ToListAsync(),
            Products: await _db.Products.ToListAsync()
            );
            return View(model); // �������� ������ �������������
        }



        [Authorize(Roles = "Administrators")]
        [Route("private")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// ����������� ���������� � ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("�� ������ ������� id �������� ��� ��������� � ��������");
            }
            Product? model = await _db.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (model == null)
            {
                return NotFound($"ProductId {id} �� ������");
            }
            return View(model); // ������� ������ ��� ��������� � ���������� ���������
        }

        public IActionResult ModelBinding()
        {
            return View(); // �������� � ������
        }
        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            //return View(thing); // ����������� � ������� ������
            HomeModelBindingViewModel model = new(
                thing,
                !ModelState.IsValid,
                ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage));
            return View(model);
        }

        public async Task<IActionResult> ProductsThatCostsMoreThan(decimal? price)
        {
            if (!price.HasValue)
                return BadRequest("�� ������ ������� ���� �������� � ������� �� ������!");

            IEnumerable<Product> model = await _db.Products.Include(p => p.Category)
                                                     .Include(p => p.Supplier)
                                                     .Where(p => p.UnitPrice > price)
                                                     .ToListAsync();
            if (!model.Any())
                return NotFound($"� �� �� ������� ��������� � ����� ���� {price}");
            ViewData["MaxPrice"] = price.Value.ToString("C"); // ��������� ������ ���������� �������
            return View(model);
        }

        public async Task<IActionResult> CategoryDetail(int? categoryId)
        {
            if (!categoryId.HasValue)
                return BadRequest($"�� ������ ������� Id ��������, ��������� Id = {categoryId}");

            IEnumerable<Product> model = await _db.Products.Include(p => p.Category)
                                                     .Include(p => p.Supplier)
                                                     .Where(p => p.CategoryId
                                                     .Equals(categoryId))
                                                     .ToListAsync();

            if (!model.Any()) return NotFound("� ���� ������ ��� �������� �� ������ ���������");
            ViewData["CategoryName"] = model.Select(p => p.Category.CategoryName).FirstOrDefault();
            return View(model);
        }

    }
}

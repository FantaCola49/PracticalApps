using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Packt.Shared; // NortwindContext

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
        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel
            (
            VisitorCount: new Random().Next(1, 1001),
            Categories: _db.Categories.ToList(),
            Products: _db.Products.ToList()
            );
            return View(model); // передача модели представления
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
        /// Отображение информации о продукте
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Вы должны указать id продукта при обращении к странице");
            }
            Product? model = _db.Products.SingleOrDefault(p => p.ProductId == id);
            if (model == null)
            {
                return NotFound($"ProductId {id} не найден");
            }
            return View(model); // передаём модель для просмотра и возвращаем результат
        }

        public IActionResult ModelBinding()
        {
            return View(); // страница с формой
        }
        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            //return View(thing); // привязанный к моделти объект
            HomeModelBindingViewModel model = new(
                thing,
                !ModelState.IsValid,
                ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage));
            return View(model);
        }
    }
}

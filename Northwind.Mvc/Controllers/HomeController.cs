using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Models;
using Packt.Shared; // NortwindContext
using System.Diagnostics;
using System.Net.Http;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _db;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, NorthwindContext db, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _db = db;
            _clientFactory = httpClientFactory;
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
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Вы должны указать id продукта при обращении к странице");
            }
            Product? model = await _db.Products.SingleOrDefaultAsync(p => p.ProductId == id);
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

        public async Task<IActionResult> ProductsThatCostsMoreThan(decimal? price)
        {
            if (!price.HasValue)
                return BadRequest("Вы должны указать цену продукта в запросе на сервер!");

            IEnumerable<Product> model = await _db.Products.Include(p => p.Category)
                                                     .Include(p => p.Supplier)
                                                     .Where(p => p.UnitPrice > price)
                                                     .ToListAsync();
            if (!model.Any())
                return NotFound($"В БД не найдено продуктов с ценой ниже {price}");
            ViewData["MaxPrice"] = price.Value.ToString("C"); // установим валюту локального региона
            return View(model);
        }

        public async Task<IActionResult> CategoryDetail(int? categoryId)
        {
            if (!categoryId.HasValue)
                return BadRequest($"Вы должны указать Id категори, пришедший Id = {categoryId}");

            IEnumerable<Product> model = await _db.Products.Include(p => p.Category)
                                                     .Include(p => p.Supplier)
                                                     .Where(p => p.CategoryId
                                                     .Equals(categoryId))
                                                     .ToListAsync();

            if (!model.Any()) return NotFound("В базе данных нет сведений по данной категории");
            ViewData["CategoryName"] = model.Select(p => p.Category.CategoryName).FirstOrDefault();
            return View(model);
        }

        /// <summary>
        /// Получить информацию о клиентах в определённой стране через Web.Api
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<IActionResult> Customers(string country)
        {
            string uri;
            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "Покупатели со всего белого света";
                uri = "api/customers";
            }
            else
            {
                ViewData["Title"] = $"Покупатели в {country}";
                uri = $"api/customers/?country={country}";
            }
            // правильно инициализируем клиент через http - фабрику
            HttpClient client = _clientFactory.CreateClient(name: "Northwind.WebApi");
            // создали запрос
            HttpRequestMessage request = new(
                method: HttpMethod.Get,
                requestUri: uri);
            // получаем ответ
            HttpResponseMessage response = await client.SendAsync(request);
            // запарсили json - ответ в модель
            IEnumerable<Customer>? model = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
            return View(model);
        }
    }
}

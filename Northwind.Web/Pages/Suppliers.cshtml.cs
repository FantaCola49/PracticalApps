using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        /// <summary>
        /// Список поставщиков
        /// </summary>
        public IEnumerable<Supplier> _suppliers { get; set; }
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private NorthwindContext _db;

        /// <summary>
        /// Один поставщик
        /// </summary>
        [BindProperty]
        public Supplier? _supplier { get; set; }

        public SuppliersModel(NorthwindContext injectedDataBase)
        {
            _db = injectedDataBase;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Suppliers";
            // Список поставщиков
            _suppliers = _db.Suppliers.OrderBy(c => c.SupplierId).ThenBy(c => c.CompanyName);
        }
        public IActionResult OnPost()
        {
            if ((_supplier is not null) && ModelState.IsValid)
            {
                Console.WriteLine("Провалились в метод OnPost от SuppliersModel!");
                _db.Suppliers.Add(_supplier);
                Console.WriteLine("Добавили поставщика");
                _db.SaveChanges();
                Console.WriteLine("Сохранили!");
                return RedirectToAction("/suppliers");
            }
            else
            {
                return Page(); // обратно на исходную страницу
            }
        }
    }
}

using Packt.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Web.Pages
{
    public class CustomersModel : PageModel
    {
        /// <summary>
        /// Список клиентов
        /// </summary>
        public IEnumerable<Customer> _customers { get; set; }
        
        /// <summary>
        /// Клиент
        /// </summary>
        [BindProperty]
        public Customer? Customer { get; set; }
        
        /// <summary>
        /// Контекст БД
        /// </summary>
        private NorthwindContext _db;
        
        public CustomersModel(NorthwindContext injectedDB)
        {
            _db = injectedDB;
        }
        /// <summary>
        /// Обработчик запроса Get
        /// </summary>
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Customers";
            _customers = _db.Customers.OrderBy(c => c.Country);
        }
    }
}

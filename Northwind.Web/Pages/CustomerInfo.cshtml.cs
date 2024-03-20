using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class CustomerInfoModel : PageModel
    {
        /// <summary>
        /// Клиент
        /// </summary>
        [BindProperty]
        public Customer? customer { get; set; }
        /// <summary>
        /// Id клиента
        /// </summary>
        private string customerId { get; set; }
        /// <summary>
        /// Контекст БД
        /// </summary>
        private NorthwindContext _db;
        public CustomerInfoModel(NorthwindContext db)
        {
            _db = db;
        }
        public void OnGet(string custId)
        {
            customerId = custId;
            customer = _db.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
        }
    }
}

using Packt.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Web.Pages
{
    public class CustomersModel : PageModel
    {
        /// <summary>
        /// ������ ��������
        /// </summary>
        public IEnumerable<Customer> _customers { get; set; }
        
        /// <summary>
        /// ������
        /// </summary>
        [BindProperty]
        public Customer? Customer { get; set; }
        
        /// <summary>
        /// �������� ��
        /// </summary>
        private NorthwindContext _db;
        
        public CustomersModel(NorthwindContext injectedDB)
        {
            _db = injectedDB;
        }
        /// <summary>
        /// ���������� ������� Get
        /// </summary>
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Customers";
            _customers = _db.Customers.OrderBy(c => c.Country);
        }
    }
}

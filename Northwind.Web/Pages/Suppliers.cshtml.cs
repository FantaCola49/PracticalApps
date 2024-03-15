using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        /// <summary>
        /// Список поставщиков
        /// </summary>
        public IEnumerable<Supplier> Suppliers { get; set; }
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private NorthwindContext db;
        
        public SuppliersModel(NorthwindContext injectedDataBase)
        {
            db = injectedDataBase;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Suppliers";
            // Список поставщиков
            Suppliers = db.Suppliers.OrderBy(c => c.SupplierId).ThenBy(c => c.CompanyName);
        }
    }
}

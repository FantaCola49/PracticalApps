using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        /// <summary>
        /// ������ �����������
        /// </summary>
        public IEnumerable<Supplier> Suppliers { get; set; }
        /// <summary>
        /// �������� ���� ������
        /// </summary>
        private NorthwindContext db;
        
        public SuppliersModel(NorthwindContext injectedDataBase)
        {
            db = injectedDataBase;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Suppliers";
            // ������ �����������
            Suppliers = db.Suppliers.OrderBy(c => c.SupplierId).ThenBy(c => c.CompanyName);
        }
    }
}

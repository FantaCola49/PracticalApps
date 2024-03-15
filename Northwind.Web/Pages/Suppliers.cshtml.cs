using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        /// <summary>
        /// ������ �����������
        /// </summary>
        public IEnumerable<Supplier> _suppliers { get; set; }
        /// <summary>
        /// �������� ���� ������
        /// </summary>
        private NorthwindContext _db;

        /// <summary>
        /// ���� ���������
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
            // ������ �����������
            _suppliers = _db.Suppliers.OrderBy(c => c.SupplierId).ThenBy(c => c.CompanyName);
        }
        public IActionResult OnPost()
        {
            if ((_supplier is not null) && ModelState.IsValid)
            {
                Console.WriteLine("����������� � ����� OnPost �� SuppliersModel!");
                _db.Suppliers.Add(_supplier);
                Console.WriteLine("�������� ����������");
                _db.SaveChanges();
                Console.WriteLine("���������!");
                return RedirectToAction("/suppliers");
            }
            else
            {
                return Page(); // ������� �� �������� ��������
            }
        }
    }
}

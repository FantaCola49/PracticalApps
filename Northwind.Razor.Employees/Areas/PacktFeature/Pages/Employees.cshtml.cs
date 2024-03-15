using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace PacktFeature.Pages
{
    public class EmployeesPageModel : PageModel
    {
        /// <summary>
        /// Контекст работы с БД
        /// </summary>
        NorthwindContext _db;
        public EmployeesPageModel(NorthwindContext injectedDB)
        {
            _db = injectedDB;
        }
        /// <summary>
        /// Сотрудники
        /// </summary>
        public Employee[] Employees { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "Сотрудники";
            Employees = _db.Employees.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToArray();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class CustomerInfoModel : PageModel
    {
        #region Fields
        /// <summary>
        /// Клиент
        /// </summary>
        [BindProperty]
        public Customer? customer { get; set; }

        /// <summary>
        /// Заказ клиента
        /// </summary>
        [BindProperty]
        public Order? order { get; set; }

        /// <summary>
        /// Компания перевозки 
        /// </summary>
        [BindProperty]
        public Shipper shipper { get; set; }

        /// <summary>
        /// Список заказов
        /// </summary>
        public IEnumerable<Order> _orders;

        /// <summary>
        /// Список компаний доставки
        /// </summary>
        public IEnumerable<Shipper> _shippers;

        /// <summary>
        /// Id клиента
        /// </summary>
        private string customerId { get; set; }
        /// <summary>
        /// Контекст БД
        /// </summary>
        private NorthwindContext _db;

        private Shipper undefinedShipper = new()
        {
            CompanyName = "NONE"
        };
        #endregion
        #region Ctor
        public CustomerInfoModel(NorthwindContext db)
        {
            _db = db;
        }
        #endregion
        public void OnGet(string custId)
        {
            ViewData["Title"] = "Northwind B2B";
            customerId = custId;
            customer = _db.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
            _orders = _db.Orders.Where(o => o.CustomerId == customerId).OrderBy(o => o.OrderId).ToList();
        }
        /// <summary>
        /// Предоставит курьра
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Shipper GetShipperById(int? Id)
        {
            if (Id is null)
                return undefinedShipper;
            Shipper shipper = _db.Shippers.Where(s => s.ShipperId == Id).FirstOrDefault();
            if (shipper is not null)
                return shipper;
            else
                return undefinedShipper;
        }
    }
}

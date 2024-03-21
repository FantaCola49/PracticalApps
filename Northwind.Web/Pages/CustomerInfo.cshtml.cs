using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages
{
    public class CustomerInfoModel : PageModel
    {
        #region Fields
        /// <summary>
        /// ������
        /// </summary>
        [BindProperty]
        public Customer? customer { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        [BindProperty]
        public Order? order { get; set; }

        /// <summary>
        /// �������� ��������� 
        /// </summary>
        [BindProperty]
        public Shipper shipper { get; set; }

        /// <summary>
        /// ������ �������
        /// </summary>
        public IEnumerable<Order> _orders;

        /// <summary>
        /// ������ �������� ��������
        /// </summary>
        public IEnumerable<Shipper> _shippers;

        /// <summary>
        /// Id �������
        /// </summary>
        private string customerId { get; set; }
        /// <summary>
        /// �������� ��
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
        /// ����������� ������
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

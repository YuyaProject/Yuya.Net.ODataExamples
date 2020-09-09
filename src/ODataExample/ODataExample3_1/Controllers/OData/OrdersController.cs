using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class OrdersController : ODataBaseController<Order, int>
	{
		private readonly NorthwindDbContext _db;

		public OrdersController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}


		[HttpGet]
		[EnableQuery]
		public IQueryable<OrderDetail> GetOrderDetails(int key)
		{
			return _db.OrderDetails.Where(x => x.OrderId == key);
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<Shipper> GetShipVia(int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.ShipVia));
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<Customer> GetCustomer(int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.Customer));
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<Employee> GetEmployee(int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.Employee));
		}
	}
}
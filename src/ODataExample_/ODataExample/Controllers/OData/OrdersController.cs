using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Orders")]
	public class OrdersController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public OrdersController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		[EnableQuery]
		public virtual IQueryable<Order> Get()
		{
			return _db.Orders;
		}

		[EnableQuery]
		public virtual SingleResult<Order> Get([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key));
		}

		[EnableQuery]
		public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] int key)
		{
			return _db.OrderDetails.Where(x => x.OrderId == key);
		}

		[EnableQuery]
		public SingleResult<Shipper> GetShipVia([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.ShipVia));
		}

		[EnableQuery]
		public SingleResult<Customer> GetCustomer([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.Customer));
		}

		[EnableQuery]
		public SingleResult<Employee> GetEmployee([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key).Select(e => e.Employee));
		}
	}
}
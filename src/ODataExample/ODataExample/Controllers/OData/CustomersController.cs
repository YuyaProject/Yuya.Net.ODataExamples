using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Customers")]
	public class CustomersController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public CustomersController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public virtual IQueryable<Customer> Get()
		{
			return _db.Customers;
		}

		[EnableQuery]
		public virtual SingleResult<Customer> Get([FromODataUri] string key)
		{
			return SingleResult.Create(_db.Customers.Where(e => e.Id == key));
		}

		public IQueryable<Order> GetOrders([FromODataUri] string key)
		{
			return _db.Orders.Where(x => x.CustomerId == key);
		}
	}
}
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

		public virtual IQueryable<Order> Get()
		{
			return _db.Orders;
		}

		[EnableQuery]
		public virtual SingleResult<Order> Get([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Orders.Where(e => e.Id == key));
		}
	}
}
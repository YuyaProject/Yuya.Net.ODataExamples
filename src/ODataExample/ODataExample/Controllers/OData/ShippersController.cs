using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Shippers")]
	public class ShippersController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public ShippersController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public virtual IQueryable<Shipper> Get()
		{
			return _db.Shippers;
		}

		[EnableQuery]
		public virtual SingleResult<Shipper> Get([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Shippers.Where(e => e.Id == key));
		}

		public IQueryable<Order> GetOrders([FromODataUri] int key)
		{
			return _db.Orders.Where(x => x.ShipViaId == key);
		}
	}
}
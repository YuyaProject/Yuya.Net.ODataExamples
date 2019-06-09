using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
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

		public virtual IQueryable<Shipper> Get(ODataQueryOptions<Shipper> options)
		{
			return _db.Shippers;
		}

		[EnableQuery]
		public virtual SingleResult<Shipper> Get([FromODataUri] int key, ODataQueryOptions<Shipper> options)
		{
			return SingleResult.Create(_db.Shippers.Where(e => e.Id == key));
		}
	}
}
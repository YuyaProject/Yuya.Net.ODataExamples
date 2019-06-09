using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Suppliers")]
	public class SuppliersController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public SuppliersController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public virtual IQueryable<Supplier> Get(ODataQueryOptions<Supplier> options)
		{
			return _db.Suppliers;
		}

		public virtual SingleResult<Supplier> Get([FromODataUri] int key, ODataQueryOptions<Supplier> options)
		{
			return SingleResult.Create(_db.Suppliers.Where(e => e.Id == key));
		}
	}
}
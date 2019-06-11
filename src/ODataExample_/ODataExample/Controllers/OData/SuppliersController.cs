using Microsoft.AspNet.OData;
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

		[EnableQuery]
		public virtual IQueryable<Supplier> Get()
		{
			return _db.Suppliers;
		}

		[EnableQuery]
		public virtual SingleResult<Supplier> Get([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Suppliers.Where(e => e.Id == key));
		}

		[EnableQuery]
		public IQueryable<Product> GetProducts([FromODataUri] int key)
		{
			return _db.Products.Where(x => x.SupplierId == key);
		}
	}
}
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Products")]
	public class ProductsController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public ProductsController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public virtual IQueryable<Product> Get(ODataQueryOptions<Product> options)
		{
			return _db.Products;
		}

		[EnableQuery]
		public virtual SingleResult<Product> Get([FromODataUri] int key, ODataQueryOptions<Product> options)
		{
			return SingleResult.Create(_db.Products.Where(e => e.Id == key));
		}
	}
}
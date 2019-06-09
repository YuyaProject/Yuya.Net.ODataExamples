using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Territories")]
	public class TerritoriesController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public TerritoriesController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public virtual IQueryable<Territory> Get(ODataQueryOptions<Territory> options)
		{
			return _db.Territories;
		}

		[EnableQuery]
		public virtual SingleResult<Territory> Get([FromODataUri] string key, ODataQueryOptions<Territory> options)
		{
			return SingleResult.Create(_db.Territories.Where(e => e.Id == key));
		}
	}
}
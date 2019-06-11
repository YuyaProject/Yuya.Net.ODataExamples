using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Regions")]
	public class RegionsController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public RegionsController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		[EnableQuery]
		public virtual IQueryable<Region> Get()
		{
			return _db.Regions;
		}

		[EnableQuery]
		public virtual SingleResult<Region> Get([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Regions.Where(e => e.Id == key));
		}

		[EnableQuery]
		public IQueryable<Territory> GetTerritories([FromODataUri] int key)
		{
			return _db.Territories.Where(x => x.RegionId == key);
		}
	}
}
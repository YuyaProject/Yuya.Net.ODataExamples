using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class RegionsController : ODataBaseController<Region, int>
	{
		private readonly NorthwindDbContext _db;

		public RegionsController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<Territory> GetTerritories( int key)
		{
			return _db.Territories.Where(x => x.RegionId == key);
		}
	}
}
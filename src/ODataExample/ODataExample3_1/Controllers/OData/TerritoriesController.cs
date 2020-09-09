using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class TerritoriesController : ODataBaseController<Territory, string>
	{
		private readonly NorthwindDbContext _db;

		public TerritoriesController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<EmployeeTerritory> GetEmployeeTerritories(string key)
		{
			return _db.EmployeeTerritories.Where(x => x.TerritoryId == key);
		}
	}
}
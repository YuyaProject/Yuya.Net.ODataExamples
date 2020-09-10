using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("Territories")]
    public class TerritoriesController : GenericController<Territory, string>
    {
        public TerritoriesController(NorthwindDbContext db) : base(db)
        {
        }

        [EnableQuery]
        public IQueryable<EmployeeTerritory> GetEmployeeTerritories([FromODataUri] string key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.EmployeeTerritories.Where(x => x.TerritoryId == key);
        }
    }
}
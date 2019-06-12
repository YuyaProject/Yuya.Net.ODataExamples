using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("Regions")]
    public class RegionsController : GenericController<Region, int>
    {
        public RegionsController(NorthwindDbContext db) : base(db)
        {
        }

        [EnableQuery]
        public IQueryable<Territory> GetTerritories([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.Territories.Where(x => x.RegionId == key);
        }
    }
}
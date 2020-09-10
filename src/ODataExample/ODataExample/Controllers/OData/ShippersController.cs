using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("Shippers")]
    public class ShippersController : GenericController<Shipper, int>
    {
        public ShippersController(NorthwindDbContext db) : base(db)
        {
        }

        [EnableQuery]
        public IQueryable<Order> GetOrders([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.Orders.Where(x => x.ShipViaId == key);
        }
    }
}
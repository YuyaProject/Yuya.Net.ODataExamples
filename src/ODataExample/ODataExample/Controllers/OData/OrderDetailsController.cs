using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("OrderDetails")]
    public class OrderDetailsController : ODataController
    {
        private readonly NorthwindDbContext _db;

        public OrderDetailsController(NorthwindDbContext db)
        {
            _db = db;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [EnableQuery]
        public virtual IQueryable<OrderDetail> Get()
        {
            return _db.OrderDetails;
        }
    }
}
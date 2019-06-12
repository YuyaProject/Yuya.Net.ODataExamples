using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("Suppliers")]
    public class SuppliersController : GenericController<Supplier, int>
    {
        public SuppliersController(NorthwindDbContext db) : base(db)
        {
        }

        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.Products.Where(x => x.SupplierId == key);
        }
    }
}
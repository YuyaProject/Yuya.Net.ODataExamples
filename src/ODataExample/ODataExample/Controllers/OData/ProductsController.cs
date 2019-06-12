using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
    [ODataRoutePrefix("Products")]
    public class ProductsController : GenericController<Product, int>
    {
        public ProductsController(NorthwindDbContext db) : base(db)
        {
        }

        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return _db.OrderDetails.Where(x => x.ProductId == key);
        }

        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return SingleResult.Create(_db.Products.Where(e => e.Id == key).Select(e => e.Supplier));
        }

        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return SingleResult.Create(_db.Products.Where(e => e.Id == key).Select(e => e.Category));
        }
    }
}
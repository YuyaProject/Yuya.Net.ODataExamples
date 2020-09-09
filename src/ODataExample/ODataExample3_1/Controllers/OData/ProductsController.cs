using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class ProductsController : ODataBaseController<Product, int>
	{
		private readonly NorthwindDbContext _db;

		public ProductsController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}


		[HttpGet]
		[EnableQuery]
		public IQueryable<OrderDetail> GetOrderDetails( int key)
		{
			return _db.OrderDetails.Where(x => x.ProductId == key);
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<Supplier> GetSupplier( int key)
		{
			return SingleResult.Create(_db.Products.Where(e => e.Id == key).Select(e => e.Supplier));
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<Category> GetCategory( int key)
		{
			return SingleResult.Create(_db.Products.Where(e => e.Id == key).Select(e => e.Category));
		}
	}
}
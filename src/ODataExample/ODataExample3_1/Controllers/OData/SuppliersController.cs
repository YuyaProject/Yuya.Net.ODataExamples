using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class SuppliersController : ODataBaseController<Supplier, int>
	{
		private readonly NorthwindDbContext _db;

		public SuppliersController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<Product> GetProducts(int key)
		{
			return _db.Products.Where(x => x.SupplierId == key);
		}
	}
}
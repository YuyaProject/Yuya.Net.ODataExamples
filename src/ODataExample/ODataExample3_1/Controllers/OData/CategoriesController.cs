using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class CategoriesController : ODataBaseController<Category, int>
	{
		private readonly NorthwindDbContext _db;

		public CategoriesController(NorthwindDbContext db) : base(db)
		{
			_db = db;
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<Product> GetProducts(int key)
		{
			return _db.Products.Where(x => x.CategoryId == key);
		}
	}
}
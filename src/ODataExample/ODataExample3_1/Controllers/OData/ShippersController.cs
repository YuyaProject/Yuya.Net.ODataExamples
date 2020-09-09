using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class ShippersController : ODataBaseController<Shipper, int>
	{
		private readonly NorthwindDbContext _db;

		public ShippersController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}


		[HttpGet]
		[EnableQuery]
		public IQueryable<Order> GetOrders(int key)
		{
			return _db.Orders.Where(x => x.ShipViaId == key);
		}
	}
}
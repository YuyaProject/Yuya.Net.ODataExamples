using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Customers")]
	public class CustomersController : GenericController<Customer, string>
	{

		public CustomersController(NorthwindDbContext db): base(db)
		{
		}

		[EnableQuery]
		public IQueryable<Order> GetOrders([FromODataUri] string key)
		{
			return _db.Orders.Where(x => x.CustomerId == key);
		}
	}
}
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample.Controllers.OData
{
	[ODataRoutePrefix("Employees")]
	public class EmployeesController : ODataController
	{
		private readonly NorthwindDbContext _db;

		public EmployeesController(NorthwindDbContext db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public virtual IQueryable<Employee> GetEmployees()
		{
			return _db.Employees;
		}

		[EnableQuery]
		public virtual SingleResult<Employee> GetEmployee([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Employees.Where(e => e.Id == key));
		}

		public IQueryable<Order> GetOrders([FromODataUri] int key)
		{
			return _db.Orders.Where(x => x.EmployeeId == key);
		}

		public IQueryable<Employee> GetEmployees([FromODataUri] int key)
		{
			return _db.Employees.Where(x => x.ReportsToId == key);
		}

		public IQueryable<EmployeeTerritory> GetEmployeeTerritories([FromODataUri] int key)
		{
			return _db.EmployeeTerritories.Where(x => x.EmployeeId == key);
		}

		public SingleResult<Employee> GetReportsTo([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Employees.Where(e => e.Id == key).Select(e => e.ReportsTo));
		}
	}
}
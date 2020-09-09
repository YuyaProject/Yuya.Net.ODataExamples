using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class EmployeesController : ODataBaseController<Employee, int>
	{
		private readonly NorthwindDbContext _db;

		public EmployeesController(NorthwindDbContext db) : base(db)
		{
			_db = db;
			_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<Order> GetOrders([FromODataUri] int key)
		{
			return _db.Orders.Where(x => x.EmployeeId == key);
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<Employee> GetEmployees([FromODataUri] int key)
		{
			return _db.Employees.Where(x => x.ReportsToId == key);
		}

		[HttpGet]
		[EnableQuery]
		public IQueryable<EmployeeTerritory> GetEmployeeTerritories([FromODataUri] int key)
		{
			return _db.EmployeeTerritories.Where(x => x.EmployeeId == key);
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<Employee> GetReportsTo([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Employees.Where(e => e.Id == key).Select(e => e.ReportsTo));
		}
	}
}
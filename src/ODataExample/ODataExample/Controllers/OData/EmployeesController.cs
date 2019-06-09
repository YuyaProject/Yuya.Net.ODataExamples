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

		public virtual IQueryable<Employee> Get()
		{
			return _db.Employees;
		}

		[EnableQuery]
		public virtual SingleResult<Employee> Get([FromODataUri] int key)
		{
			return SingleResult.Create(_db.Employees.Where(e => e.Id == key));
		}
	}
}
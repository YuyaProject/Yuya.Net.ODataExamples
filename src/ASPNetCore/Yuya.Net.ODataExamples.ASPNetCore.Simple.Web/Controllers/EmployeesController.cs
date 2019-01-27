using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  public class EmployeesController : ODataController
  {
    private NorthwindDbContext _db;

    public EmployeesController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    public IEnumerable<Employee> Get()
    {
      return _db.Employees;
    }

    [EnableQuery]
    public SingleResult<Employee> Get(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key));
    }
  }
}

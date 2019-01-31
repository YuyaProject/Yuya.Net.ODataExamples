using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Employees")]
  public class EmployeesController : ODataController
  {
    private NorthwindDbContext _db;

    public EmployeesController(NorthwindDbContext context)
    {
      _db = context;
    }

    [ODataRoute]
    [EnableQuery] 
    public IEnumerable<Employee> Get()
    {
      return _db.Employees;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Employee> Get(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key));
    }

    [ODataRoute("({key})/EmployeeID")]
    [EnableQuery]
    public SingleResult<int> GetEmployeeID(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.EmployeeID));
    }

    [ODataRoute("({key})/FirstName")]
    [EnableQuery]
    public SingleResult<string> GetFirstName(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.FirstName));
    }

    [ODataRoute("({key})/LastName")]
    [EnableQuery]
    public SingleResult<string> GetLastName(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.LastName));
    }

    [ODataRoute("({key})/Title")]
    [EnableQuery]
    public SingleResult<string> GetTitle(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.Title));
    }

    [ODataRoute("({key})/TitleOfCourtesy")]
    [EnableQuery]
    public SingleResult<string> GetTitleOfCourtesy(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.TitleOfCourtesy));
    }

    [ODataRoute("({key})/BirthDate")]
    [EnableQuery]
    public SingleResult<DateTime?> GetBirthDate(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.BirthDate));
    }

    [ODataRoute("({key})/HireDate")]
    [EnableQuery]
    public SingleResult<DateTime?> GetHireDate(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.HireDate));
    }

    [ODataRoute("({key})/Address")]
    [EnableQuery]
    public SingleResult<string> GetAddress(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.Address));
    }

    [ODataRoute("({key})/City")]
    [EnableQuery]
    public SingleResult<string> GetCity(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.City));
    }

    [ODataRoute("({key})/Region")]
    [EnableQuery]
    public SingleResult<string> GetRegion(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.Region));
    }

    [ODataRoute("({key})/PostalCode")]
    [EnableQuery]
    public SingleResult<string> GetPostalCode(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.PostalCode));
    }

    [ODataRoute("({key})/Country")]
    [EnableQuery]
    public SingleResult<string> GetCountry(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.Country));
    }

    [ODataRoute("({key})/HomePhone")]
    [EnableQuery]
    public SingleResult<string> GetHomePhone(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.HomePhone));
    }

    [ODataRoute("({key})/Extension")]
    [EnableQuery]
    public SingleResult<string> GetExtension(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.Extension));
    }

    [ODataRoute("({key})/Notes")]
    [EnableQuery]
    public SingleResult<string> GetNotes(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.Notes));
    }

    [ODataRoute("({key})/PhotoPath")]
    [EnableQuery]
    public SingleResult<string> GetPhotoPath(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.PhotoPath));
    }

    [ODataRoute("({key})/ReportsToEmployee")]
    [EnableQuery]
    public SingleResult<Employee> GetReportsToEmployee(int key)
    {
      return SingleResult.Create(_db.Employees.Where(c => c.EmployeeID == key).Select(x => x.ReportsToEmployee));
    }

    [ODataRoute("({key})/ReportsToEmployee")]
    [EnableQuery]
    public IEnumerable<Employee> GetReportingEmployees(int key)
    {
      return _db.Employees.Where(c => c.ReportsTo == key);
    }
  }
}

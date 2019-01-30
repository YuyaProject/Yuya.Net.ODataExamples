using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Customers")]
  public class CustomersController : ODataController
  {
    private NorthwindDbContext _db;

    public CustomersController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    [ODataRoute]
    public IEnumerable<Customer> Get()
    {
      return _db.Customers;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Customer> Get(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key));
    }

    [ODataRoute("({key})/CustomerID")]
    [EnableQuery]
    public SingleResult<string> GetCustomerID(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.CustomerID));
    }

    [ODataRoute("({key})/CompanyName")]
    [EnableQuery]
    public SingleResult<string> GetCompanyName(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.CompanyName));
    }

    [ODataRoute("({key})/ContactName")]
    [EnableQuery]
    public SingleResult<string> GetContactName(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.ContactName));
    }

    [ODataRoute("({key})/ContactTitle")]
    [EnableQuery]
    public SingleResult<string> GetContactTitle(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.ContactTitle));
    }

    [ODataRoute("({key})/Address")]
    [EnableQuery]
    public SingleResult<string> GetAddress(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.Address));
    }

    [ODataRoute("({key})/City")]
    [EnableQuery]
    public SingleResult<string> GetCity(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.City));
    }

    [ODataRoute("({key})/Region")]
    [EnableQuery]
    public SingleResult<string> GetRegion(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.Region));
    }

    [ODataRoute("({key})/PostalCode")]
    [EnableQuery]
    public SingleResult<string> GetPostalCode(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.PostalCode));
    }

    [ODataRoute("({key})/Country")]
    [EnableQuery]
    public SingleResult<string> GetCountry(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.Country));
    }

    [ODataRoute("({key})/Phone")]
    [EnableQuery]
    public SingleResult<string> GetPhone(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.Phone));
    }

    [ODataRoute("({key})/Fax")]
    [EnableQuery]
    public SingleResult<string> GetFax(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key).Select(x => x.Fax));
    }

    [ODataRoute("({key})/Orders")]
    [EnableQuery]
    public IEnumerable<Order> GetOrders(string key)
    {
      return _db.Orders.Where(c => c.CustomerID == key);
    }
  }
}

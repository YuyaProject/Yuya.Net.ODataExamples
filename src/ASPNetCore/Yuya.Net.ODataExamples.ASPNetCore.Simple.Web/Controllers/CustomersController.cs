using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  public class CustomersController : ODataController
  {
    private NorthwindDbContext _db;

    public CustomersController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    public IEnumerable<Customer> Get()
    {
      return _db.Customers;
    }

    [EnableQuery]
    public SingleResult<Customer> Get(string key)
    {
      return SingleResult.Create(_db.Customers.Where(c => c.CustomerID == key));
    }
  }
}

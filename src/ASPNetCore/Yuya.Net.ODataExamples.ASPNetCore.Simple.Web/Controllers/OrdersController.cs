using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Orders")]
  public class OrdersController : ODataController
  {
    private NorthwindDbContext _db;

    public OrdersController(NorthwindDbContext context)
    {
      _db = context;
    }

    [ODataRoute()]
    [EnableQuery]
    public IEnumerable<Order> Get()
    {
      return _db.Orders;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Order> Get(int key)
    {
      return SingleResult.Create(_db.Orders.Where(c => c.OrderID == key));
    }
  }
}

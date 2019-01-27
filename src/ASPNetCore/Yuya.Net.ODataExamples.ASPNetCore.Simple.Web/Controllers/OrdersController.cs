using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  public class OrdersController : ODataController
  {
    private NorthwindDbContext _db;

    public OrdersController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    public IEnumerable<Order> Get()
    {
      return _db.Orders;
    }

    [EnableQuery]
    public SingleResult<Order> Get(int key)
    {
      return SingleResult.Create(_db.Orders.Where(c => c.OrderID == key));
    }
  }
}

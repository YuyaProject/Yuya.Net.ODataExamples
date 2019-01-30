using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Shippers")]
  public class ShippersController : ODataController
  {
    private NorthwindDbContext _db;

    public ShippersController(NorthwindDbContext context)
    {
      _db = context;
    }

    [ODataRoute]
    [EnableQuery]
    public IEnumerable<Shipper> Get()
    {
      return _db.Shippers;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Shipper> Get(int key)
    {
      return SingleResult.Create(_db.Shippers.Where(c => c.ShipperID == key));
    }
  }
}

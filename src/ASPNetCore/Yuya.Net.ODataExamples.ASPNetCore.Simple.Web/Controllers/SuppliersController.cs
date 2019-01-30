using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Suppliers")]
  public class SuppliersController : ODataController
  {
    private NorthwindDbContext _db;

    public SuppliersController(NorthwindDbContext context)
    {
      _db = context;
    }

    [ODataRoute]
    [EnableQuery]
    public IEnumerable<Supplier> Get()
    {
      return _db.Suppliers;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Supplier> Get(int key)
    {
      return SingleResult.Create(_db.Suppliers.Where(c => c.SupplierID == key));
    }
  }
}

using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Products")]
  public class ProductsController : ODataController
  {
    private NorthwindDbContext _db;

    public ProductsController(NorthwindDbContext context)
    {
      _db = context;
    }

    [ODataRoute]
    [EnableQuery]
    public IEnumerable<Product> Get()
    {
      return _db.Products;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Product> Get(int key)
    {
      ODataPath path = Request.ODataFeature().Path;
      return SingleResult.Create(_db.Products.Where(c => c.ProductID == key));
    }
  }
}

using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  public class ProductsController : ODataController
  {
    private NorthwindDbContext _db;

    public ProductsController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    public IEnumerable<Product> Get()
    {
      return _db.Products;
    }

    [EnableQuery]
    public SingleResult<Product> Get(int key)
    {
      return SingleResult.Create(_db.Products.Where(c => c.ProductID == key));
    }
  }
}

using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  public class CategoriesController : ODataController
  {
    private NorthwindDbContext _db;

    public CategoriesController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    public IEnumerable<Category> Get()
    {
      return _db.Categories;
    }

    [EnableQuery]
    public SingleResult<Category> Get(int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key));
    }
  }
}

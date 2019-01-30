using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Categories")]
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

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Category> Get([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key));
    }



    [ODataRoute("({key})/CategoryID")]
    [EnableQuery]
    public SingleResult<int> GetCategoryID([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x => x.CategoryID));
    }

    [ODataRoute("({key})/CategoryName")]
    [EnableQuery]
    public SingleResult<string> GetCategoryName([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x => x.CategoryName));
    }

    [ODataRoute("({key})/Description")]
    [EnableQuery]
    public SingleResult<string> GetDescription([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x => x.Description));
    }

    [ODataRoute("({key})/Products")]
    [EnableQuery]
    public IEnumerable<Product> GetProducts([FromODataUri] int key)
    {
      return _db.Products.Where(c => c.CategoryID == key);
    }

  }
}

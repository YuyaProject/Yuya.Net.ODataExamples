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



    [ODataRoute("({key})/{property}")]
    [EnableQuery]
    public SingleResult<string> GetCategoryName([FromODataUri] int key, [FromODataUri]string property)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x=>x.CategoryName));
    }

    //[ODataRoute("({key})/{propertyName}")]
    //[EnableQuery]
    //public IActionResult GetProperties([FromODataUri] int key, [FromODataUri]string propertyName)
    //{
    //  if (string.IsNullOrWhiteSpace(propertyName))
    //    return Ok(SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key)));

    //  var prop =
    //  typeof(Category)
    //    .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
    //    .Where(x => string.Compare(x.Name, propertyName, StringComparison.InvariantCultureIgnoreCase) == 0)
    //    .FirstOrDefault();
    //  if (prop == null)
    //    return NotFound();

    //  var row = _db.Categories.Where(x => x.CategoryID == key);
    //  if (row == null)
    //    return NotFound();

    //  var val = prop.GetGetMethod().Invoke(row, null);

    //  return Ok(val);
    //}

  }
}

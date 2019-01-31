using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    /// <summary>
    /// Gets the category list.
    /// </summary>
    /// <returns>the category list</returns>
    [EnableQuery]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Category>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<Category> Get()
    {
      return _db.Categories;
    }

    /// <summary>
    /// Gets the category by the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>the category</returns>
    [ODataRoute("({key})")]
    [EnableQuery]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataValue<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public SingleResult<Category> Get([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key));
    }



    [ODataRoute("({key})/CategoryID")]
    [EnableQuery]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataValue<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public SingleResult<int> GetCategoryID([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x => x.CategoryID));
    }

    [ODataRoute("({key})/CategoryName")]
    [EnableQuery]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataValue<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public SingleResult<string> GetCategoryName([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x => x.CategoryName));
    }

    [ODataRoute("({key})/Description")]
    [EnableQuery]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataValue<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public SingleResult<string> GetDescription([FromODataUri] int key)
    {
      return SingleResult.Create(_db.Categories.Where(c => c.CategoryID == key).Select(x => x.Description));
    }

    [ODataRoute("({key})/Products")]
    [EnableQuery]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Product>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<Product> GetProducts([FromODataUri] int key)
    {
      return _db.Products.Where(c => c.CategoryID == key);
    }



    [HttpPost]
    public virtual async Task<IActionResult> Post([FromBody]Category entity)
    {
      if (entity == null) return BadRequest(ModelState);

      if (TryValidateModel(entity) && !ModelState.IsValid) return BadRequest(ModelState);

      await _db.AddAsync(entity);
      await _db.SaveChangesAsync();

      return Created(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody]Category entity)
    {
      entity.CategoryID = key;
      //var entity = Request.ReadEntityFromBody<T>();

      if (!ModelState.IsValid) return BadRequest(ModelState);
      else if (key != entity.CategoryID) return BadRequest("The key from the url must match the key of the entity in the body");

      var originalT = await _db.FindAsync<Category>(key);

      if (originalT == null) return NotFound();

      Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Category> entry = _db.Entry(originalT);
      entry.CurrentValues.SetValues(entity);
      entry.State = EntityState.Modified;

      await _db.SaveChangesAsync();

      return Updated(entity);
    }

    [AcceptVerbs("PATCH", "MERGE")]
    public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Category> delta)
    {
      var originalEntity = await _db.FindAsync<Category>(key);

      if (originalEntity == null) return NotFound();

      delta.Patch(originalEntity);

      await _db.SaveChangesAsync();

      return Updated(originalEntity);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromODataUri]int key)
    {
      Category entity = await _db.FindAsync<Category>(key);
      if (entity == null)
      {
        return NotFound();
      }
      else
      {
        _db.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
      }
    }

  }
}

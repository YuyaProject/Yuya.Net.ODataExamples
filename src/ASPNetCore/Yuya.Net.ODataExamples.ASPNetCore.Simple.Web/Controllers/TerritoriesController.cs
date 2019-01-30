using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [ODataRoutePrefix("Territories")]
  public class TerritoriesController : ODataController
  {
    private NorthwindDbContext _db;

    public TerritoriesController(NorthwindDbContext context)
    {
      _db = context;
    }

    [ODataRoute]
    [EnableQuery]
    public IEnumerable<Territory> Get()
    {
      return _db.Territories;
    }

    [ODataRoute("({key})")]
    [EnableQuery]
    public SingleResult<Territory> Get(string key)
    {
      return SingleResult.Create(_db.Territories.Where(c => c.TerritoryID == key));
    }
  }
}

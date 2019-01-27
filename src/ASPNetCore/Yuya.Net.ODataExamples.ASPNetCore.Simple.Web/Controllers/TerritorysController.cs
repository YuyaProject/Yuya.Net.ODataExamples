using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  public class TerritorysController : ODataController
  {
    private NorthwindDbContext _db;

    public TerritorysController(NorthwindDbContext context)
    {
      _db = context;
    }

    [EnableQuery]
    public IEnumerable<Territory> Get()
    {
      return _db.Territories;
    }

    [EnableQuery]
    public SingleResult<Territory> Get(string key)
    {
      return SingleResult.Create(_db.Territories.Where(c => c.TerritoryID == key));
    }
  }
}

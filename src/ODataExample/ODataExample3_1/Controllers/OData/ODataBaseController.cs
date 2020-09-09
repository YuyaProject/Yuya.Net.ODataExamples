using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore.Entities;
using System.Linq;

namespace ODataExample3_1.Controllers.OData
{
	public class ODataBaseController<TEntity, TKey> : ControllerBase
		where TEntity : Entity<TKey>
	{
		private readonly DbContext _db;

		public ODataBaseController(DbContext db)
		{
			_db = db;
		}

		[HttpGet]
		[EnableQuery]
		//[EnableQuery(MaxExpansionDepth =4)]
		public IQueryable<TEntity> Get(ODataQueryOptions<TEntity> options)
		{
			if(options.Apply?.ApplyClause.Transformations.Any(x=>x.Kind == Microsoft.OData.UriParser.Aggregation.TransformationNodeKind.GroupBy) ?? false)
			{
				_db.Set<TEntity>().AsNoTracking().ToList();
			}
			return _db.Set<TEntity>().AsNoTracking();
		}

		[HttpGet]
		[EnableQuery]
		public SingleResult<TEntity> Get(TKey key)
		{
			return SingleResult.Create(_db.Set<TEntity>().AsNoTracking().Where(e => e.Id.Equals(key)));
		}
	}
}
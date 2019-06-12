using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataExample.Controllers.OData
{
    public abstract class GenericController<TEntity, TKey> : ODataController
        where TEntity : Entity<TKey>, new()
    {
        protected readonly NorthwindDbContext _db;

        protected GenericController(NorthwindDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        [EnableQuery]
        public virtual IQueryable<TEntity> Get(ODataQueryOptions<TEntity> options)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var returnList = _db.Set<TEntity>();
            return returnList;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        [EnableQuery]
        public virtual SingleResult<TEntity> Get([FromODataUri] TKey key, ODataQueryOptions<TEntity> options)
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return SingleResult.Create(_db.Set<TEntity>().AsNoTracking().Where(e => EqualityComparer<TKey>.Default.Equals(e.Id, key)));
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<IActionResult> Post([FromBody]TEntity entity)
        {
            if (entity == null) return BadRequest(ModelState);

            if (TryValidateModel(entity) && !ModelState.IsValid) return BadRequest(ModelState);

            _db.Attach(entity).State = EntityState.Added;

            await _db.SaveChangesAsync();

            return Created(entity);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<IActionResult> Put([FromODataUri] TKey key, [FromBody]TEntity entity)
        {
            entity.Id = key;

            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!entity.Id.Equals(key)) return BadRequest("The key from the url must match the key of the entity in the body");

            var originalT = await _db.FindAsync<TEntity>(key);

            if (originalT == null) return NotFound();

            _db.ChangeTracker.TrackGraph(entity, e => e.Entry.SetState());

            await _db.SaveChangesAsync();

            return Updated(entity);
        }

        /// <summary>
        /// Patches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IActionResult> Patch([FromODataUri] int key)
        {
            var patch = Request.ReadEntityFromBody();
            var originalEntity = await _db.FindAsync<TEntity>(key);

            if (originalEntity == null) return NotFound();

            _db.ApplyChanges<TEntity, TKey>(originalEntity, patch);

            await _db.SaveChangesAsync();

            return Updated(originalEntity);
        }

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual async Task<IActionResult> Delete([FromODataUri]int key)
        {
            TEntity entity = await _db.FindAsync<TEntity>(key);
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
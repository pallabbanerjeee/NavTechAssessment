using Microsoft.EntityFrameworkCore.ChangeTracking;
using NavTechAssesment.DataAccess.Entities.Interfaces;
using NavTechAssesment.DataAccess.EntityFramework;
using NavTechAssesment.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.DataAccess.Repositories
{
    public class ConfigRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly NavTechDbContext _dbContext;

        /// <summary>
        /// Initializes a new repository with the specified db context.
        /// </summary>
        /// <param name="dbContext">The db context to be managed by the repository.</param>
        public ConfigRepository(NavTechDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #region IRepository<TEntity, TKey>

        /// <summary>
        /// Creates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The entity that was created.</returns>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var dbEntity = _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return dbEntity.Entity;
        }

        /// <summary>
        /// Creates entities in the repository.
        /// </summary>
        /// <param name="entities">The entities to be created.</param>
        /// <returns>The entities that were created.</returns>
        /// <exception cref="ArgumentNullException">Entities should not be null.</exception>
        public virtual async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var dbEntities = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                var dbEntity = _dbContext.Add(entity);
                dbEntities.Add(dbEntity);
            }
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return dbEntities.Select(x => x.Entity);
        }

        /// <summary>
        /// Gets an entity from the repository based on the specified key.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns>The entity that was created.</returns>
        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            return await _dbContext.FindAsync<TEntity>(key).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets an entity from the repository based on the specified composite key.
        /// </summary>
        /// <param name="key">The keys of the entity.</param>
        /// <returns>The entity that was created.</returns>
        public virtual async Task<TEntity> GetAsync(params object[] key)
        {
            return await _dbContext.FindAsync<TEntity>(key).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a queryable list of entities from the repository.
        /// </summary>
        /// <returns>A queryable list of entities from the repository.</returns>
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// Removes an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        public virtual async Task RemoveAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Removes entities in the repository.
        /// </summary>
        /// <param name="entities">The entities to be removed.</param>
        /// <exception cref="ArgumentNullException">Entities should not be null.</exception>
        public virtual async Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Remove entitiies from the repository that match predicate argument
        /// </summary>
        /// <param name="match">A predicate describing the entities to be removed</param>
        /// <returns></returns>
        public virtual async Task RemoveAllAsync(Func<TEntity, bool> match)
        {
            var removeQuery = _dbContext.Set<TEntity>().Where(match);
            _dbContext.RemoveRange(removeQuery);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>The entity that was updated.</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dbEntity = _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return dbEntity.Entity;
        }

        /// <summary>
        /// Updates entities in the repository.
        /// </summary>
        /// <param name="entities">The entities to be updated.</param>
        /// <returns>The entities that were updated.</returns>
        public virtual async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var dbEntities = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                var dbEntity = _dbContext.Update(entity);
                dbEntities.Add(dbEntity);
            }
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return dbEntities.Select(d => d.Entity);
        }

        #endregion IRepository<TEntity, TKey>
    }
}

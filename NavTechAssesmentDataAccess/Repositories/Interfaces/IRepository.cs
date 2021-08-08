using NavTechAssesment.DataAccess.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing entities.
    /// </summary>
    /// <typeparam name="TEntity">The type TEntity managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type TKey of the key property for the entity.</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Creates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The entity that was created.</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Creates entities in the repository.
        /// </summary>
        /// <param name="entities">The entities to be created.</param>
        /// <returns>The entities that were created.</returns>
        /// <exception cref="System.ArgumentNullException">Entities should not be null.</exception>
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Gets an entity from the repository based on the specified key.
        /// </summary>
        /// <param name="key">The ky of the entity.</param>
        /// <returns>The entity that was created.</returns>
        Task<TEntity> GetAsync(TKey key);

        /// <summary>
        /// Gets an entity from the repository based on the specified composite key.
        /// </summary>
        /// <param name="key">The ky of the entity.</param>
        /// <returns>The entity that was created.</returns>
        Task<TEntity> GetAsync(params object[] key);

        /// <summary>
        /// Gets a queryable list of entities from the repository.
        /// </summary>
        /// <returns>A queryable list of entities from the repository.</returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// Removes an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        Task RemoveAsync(TEntity entity);

        /// <summary>
        /// Removes entities in the repository.
        /// </summary>
        /// <param name="entity">The entities to be removed.</param>
        Task RemoveAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove entitiies from the repository that match predicate argument
        /// </summary>
        /// <param name="match">A predicate describing the entities to be removed</param>
        /// <returns></returns>
        Task RemoveAllAsync(Func<TEntity, bool> match);

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>The entity that was updated.</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates entities in the repository.
        /// </summary>
        /// <param name="entities">The entities to be updated.</param>
        /// <returns>The entities that were updated.</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
    }
}

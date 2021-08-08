using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.DataAccess.Entities.Interfaces
{
    /// <summary>
    /// An entity.
    /// </summary>
    /// <typeparam name="TKey">The type TKey of the key property for the entity.</typeparam>
    public interface IEntity<out TKey>
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        TKey Id { get; }
    }
}

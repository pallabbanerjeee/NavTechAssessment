using NavTechAssesment.DataAccess.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.DataAccess.Entities
{
    public partial class ConfigEntity : IEntity<Guid>
    {
        public ConfigEntity()
        {
            ConfigEntityMetadatas = new HashSet<ConfigEntityMetadata>();
        }
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public DateTime? DeletedDatetime { get; set; }
        public virtual ICollection<ConfigEntityMetadata> ConfigEntityMetadatas { get; set; }
    }
}

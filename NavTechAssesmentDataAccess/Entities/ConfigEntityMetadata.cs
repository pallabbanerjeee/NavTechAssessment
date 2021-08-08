using NavTechAssesment.DataAccess.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.DataAccess.Entities
{
    public class ConfigEntityMetadata:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public DateTime? DeletedDatetime { get; set; }

        public Guid Entity_Id { get; set; }
        public ConfigEntity ConfigEntity { get; set; }
    }
}

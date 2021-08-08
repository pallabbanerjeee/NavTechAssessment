using NavTechAssesment.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.DataTransferObjects.Request
{
    public class ConfigurationRequestDto
    {
        public string EntityName { get; set; }
        public IEnumerable<MetadataRequestDto> Metadatas { get; set; }

        public ConfigEntity ToEntity()
        {
            return new ConfigEntity
            {
                EntityName = EntityName,
                ConfigEntityMetadatas = Metadatas.Select(x=>x.ToEntity()).ToList()
            };
        }

    }
}

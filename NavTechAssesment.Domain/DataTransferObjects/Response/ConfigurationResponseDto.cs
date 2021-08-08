using NavTechAssesment.DataAccess.Entities;
using NavTechAssessment.DataTransferObjects.Response;
using System.Collections.Generic;

namespace NavTechAssesment.Domain.DataTransferObjects.Response
{
    public class ConfigurationResponseDto
    {
        public string EntityName { get; set; }
        public IEnumerable<MetadataResponseDto> Metadatas { get; set; }

        public static ConfigurationResponseDto FromEntity(ConfigEntity configEntity)
        {
            return new ConfigurationResponseDto
            {
                EntityName = configEntity.EntityName,
                Metadatas = MetadataResponseDto.FromEntity(configEntity.ConfigEntityMetadatas)
            };
        }
    }
}

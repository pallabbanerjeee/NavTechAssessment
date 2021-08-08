using NavTechAssesment.Domain.DataTransferObjects.Response;
using NavTechAssessment.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavTechAssessment.Models.Response
{
    public class ConfigurationResponseModel
    {
        public ConfigurationResponseModel(ConfigurationResponseDto configurationResponseDto)
        {
            EntityName = configurationResponseDto.EntityName;
            Fields = configurationResponseDto.Metadatas.Select(x => new MetadataResponseModel(x));
        }
        public string EntityName { get; set; }
        public IEnumerable<MetadataResponseModel> Fields { get; set; }

    }
}

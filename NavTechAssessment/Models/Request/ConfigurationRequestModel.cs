using NavTechAssesment.Domain.DataTransferObjects.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NavTechAssessment.Models.Request
{
    public class ConfigurationRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entity Name is Required")]
        public string EntityName { get; set; }
        [Required]
        public List<MetadataRequestModel> Fields { get; set; }

        public ConfigurationRequestDto ToDto()
        {
            return new ConfigurationRequestDto
            {
                EntityName = EntityName,
                Metadatas = Fields.ConvertAll(f => f.ToDto())
            };
        }
    }
}

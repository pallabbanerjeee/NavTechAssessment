using NavTechAssesment.Domain.DataTransferObjects.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavTechAssessment.Models.Request
{
    public class MetadataRequestModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage = "FieldName is Required")]
        public string FieldName { get; set; }
        [Required]
        public int MaxLength { get; set; }
        [Required]
        public bool IsRequired { get; set; }

        public MetadataRequestDto ToDto()
        {
            return new MetadataRequestDto
            {
                FieldName = FieldName,
                MaxLength = MaxLength,
                IsRequired = IsRequired
            };
        }
    }
}

using NavTechAssessment.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavTechAssessment.Models.Response
{
    public class MetadataResponseModel
    {
        public MetadataResponseModel(MetadataResponseDto metadataResponseDto)
        {
            FieldName = metadataResponseDto.FieldName;
            MaxLength = metadataResponseDto.MaxLength;
            IsRequired = metadataResponseDto.IsRequired;
        }
        public string FieldName { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }
    }
}

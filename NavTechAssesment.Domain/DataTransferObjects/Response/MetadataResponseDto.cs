using NavTechAssesment.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavTechAssessment.DataTransferObjects.Response
{
    public class MetadataResponseDto
    {
        public string FieldName { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }
        public string Source { get; set; }

        public static IEnumerable<MetadataResponseDto> FromEntity(ICollection<ConfigEntityMetadata> configEntityMetadata)
        {
            return configEntityMetadata.Select(x => new MetadataResponseDto { FieldName = x.FieldName, IsRequired = x.IsRequired, MaxLength = x.MaxLength });
        }
    }
}

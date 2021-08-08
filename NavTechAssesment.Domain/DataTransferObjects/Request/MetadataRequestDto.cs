using NavTechAssesment.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.DataTransferObjects.Request
{
    public class MetadataRequestDto
    {
        public string FieldName { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }

        public ConfigEntityMetadata ToEntity()
        {
            return new ConfigEntityMetadata
            {
                FieldName = FieldName,
                MaxLength = MaxLength,
                IsRequired = IsRequired
            };
        }
    }
}

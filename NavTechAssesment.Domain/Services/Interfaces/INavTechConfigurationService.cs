using NavTechAssesment.DataAccess.Entities;
using NavTechAssesment.Domain.DataTransferObjects.Request;
using NavTechAssesment.Domain.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Services.Interfaces
{
    public interface INavTechConfigurationService
    {
        Task<ConfigurationResponseDto> GetConfiguration(string entity);
        Task<ConfigurationResponseDto> AddNewConfiguration(ConfigurationRequestDto configurationRequestDto);
        Task<bool> DeleteConfiguration(string entity);
        Task<bool> DeleteEntityMetadata(string entity, string field);
    }
}

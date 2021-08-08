using Microsoft.EntityFrameworkCore;
using NavTechAssesment.DataAccess.Entities;
using NavTechAssesment.DataAccess.Repositories.Interfaces;
using NavTechAssesment.Domain.Clients.DataProvider.Field.Models.Response;
using NavTechAssesment.Domain.Clients.DataProvider.Interface;
using NavTechAssesment.Domain.DataTransferObjects.Request;
using NavTechAssesment.Domain.DataTransferObjects.Response;
using NavTechAssesment.Domain.Exceptions;
using NavTechAssesment.Domain.Services.Interfaces;
using NavTechAssessment.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class NavTechConfiguationService : INavTechConfigurationService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<ConfigEntity, Guid> _configEntityRepository;
        private readonly IRepository<ConfigEntityMetadata, Guid> _configEntityMetadataRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <param name="configEntityRepository"></param>
        /// <param name="configEntityMetadataRepository"></param>
        public NavTechConfiguationService(IDataProvider dataProvider,
            IRepository<ConfigEntity, Guid> configEntityRepository,
            IRepository<ConfigEntityMetadata, Guid> configEntityMetadataRepository
            )
        {
            _dataProvider = dataProvider;
            _configEntityRepository = configEntityRepository;
            _configEntityMetadataRepository = configEntityMetadataRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationRequestDto"></param>
        /// <returns></returns>
        public async Task<ConfigurationResponseDto> AddNewConfiguration(ConfigurationRequestDto configurationRequestDto)
        {
            ConfigEntity configEntityData = null;
            var entity = configurationRequestDto.ToEntity();

            var result = await _configEntityRepository.GetQueryable()
                .Include(x => x.ConfigEntityMetadatas.Where(x => x.DeletedDatetime == null))
                .FirstOrDefaultAsync(x => x.EntityName == configurationRequestDto.EntityName && x.DeletedDatetime == null).ConfigureAwait(false);

            if (result == null)
            {
                configEntityData = await _configEntityRepository.CreateAsync(entity).ConfigureAwait(false);
                return ConfigurationResponseDto.FromEntity(configEntityData);
            }

            entity.ConfigEntityMetadatas.ToList().ForEach(x => x.Entity_Id = result.Id);

            var configMetaData = await _configEntityMetadataRepository.CreateAsync(entity.ConfigEntityMetadatas).ConfigureAwait(false);
            if (configMetaData == null)
            {
                throw new ConfigEntityException("some error has occured",HttpStatusCode.BadRequest);
            }

            entity.ConfigEntityMetadatas = configMetaData.ToList();

            return ConfigurationResponseDto.FromEntity(entity);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ConfigurationResponseDto> GetConfiguration(string entity)
        {
            var result = await _dataProvider.RetrieveFieldDataAsync(entity).ConfigureAwait(false);

            if (result == null)
            {
                throw new ConfigEntityException("Data source is unable to retrieve data, please try again after sometime",HttpStatusCode.InternalServerError);
            }

            var ConfigData = await _configEntityRepository.GetQueryable()
                .Include(x => x.ConfigEntityMetadatas.Where(x => x.DeletedDatetime == null))
                .FirstOrDefaultAsync(x => x.EntityName == entity && x.DeletedDatetime == null).ConfigureAwait(false);

            if (ConfigData == null || ConfigData.ConfigEntityMetadatas == null)
            {
                throw new ConfigEntityException($"Either {entity} is deleted or it doesn't exist, please check the data again",HttpStatusCode.NotFound);
            }

            return new ConfigurationResponseDto
            {
                EntityName = entity,
                Metadatas = from a in result
                            join b in ConfigData.ConfigEntityMetadatas
                            on a.Field.ToUpper() equals b.FieldName.ToUpper()
                            select new MetadataResponseDto
                            {
                                FieldName = a.Field,
                                IsRequired = b.IsRequired,
                                MaxLength = b.MaxLength,
                                Source = a.Source
                            }
            };


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteConfiguration(string entity)
        {

            var result = await _configEntityRepository.GetQueryable()
               .Include(x => x.ConfigEntityMetadatas.Where(x => x.DeletedDatetime == null))
               .FirstOrDefaultAsync(x => x.EntityName == entity && x.DeletedDatetime == null).ConfigureAwait(false);
            if (result == null)
            {
                throw new ConfigEntityException("bad thing happend",HttpStatusCode.NotFound);
            }
            result.DeletedDatetime = DateTime.Now;

            foreach (var metadata in result.ConfigEntityMetadatas)
            {
                metadata.DeletedDatetime = DateTime.Now;
            }

            var data = await _configEntityRepository.UpdateAsync(result).ConfigureAwait(false);

            return (data != null && data.ConfigEntityMetadatas.All(x => x.DeletedDatetime != null));



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEntityMetadata(string entity, string field)
        {

            var result = await _configEntityRepository.GetQueryable()
               .Include(x => x.ConfigEntityMetadatas.Where(x => x.FieldName == field && x.DeletedDatetime == null))
               .FirstOrDefaultAsync(x => x.EntityName == entity && x.DeletedDatetime == null).ConfigureAwait(false);

            if (result == null || !result.ConfigEntityMetadatas.Any())
            {
                throw new ConfigEntityException($"Either {entity} or it's associated {field} doesn't exist", HttpStatusCode.NotFound);
            }

            foreach (var metadata in result.ConfigEntityMetadatas)
            {
                metadata.DeletedDatetime = DateTime.Now;
            }

            var data = await _configEntityRepository.UpdateAsync(result).ConfigureAwait(false);

            return data.ConfigEntityMetadatas.All(x => x.DeletedDatetime != null);


        }

        #region <<Private Methods>>
        #endregion
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NavTechAssesment.Domain.DataTransferObjects.Request;
using NavTechAssesment.Domain.DataTransferObjects.Response;
using NavTechAssesment.Domain.Exceptions;
using NavTechAssesment.Domain.Services.Interfaces;
using NavTechAssessment.Controllers;
using NavTechAssessment.DataTransferObjects.Response;
using NavTechAssessment.Models.Request;
using NavTechAssessment.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssessment.Tests.Controllers
{
    [TestClass]
    public class ConfigurationControllerTests
    {
        private ConfigurationController _controllerUnderTest;
        private ConfigurationRequestModel _configurationRequestModel;
        private ConfigurationResponseModel _configurationResponseModel;
        private List<MetadataRequestModel> _metadataRequestModels;
        private ConfigurationResponseDto _configurationResponseDto;
        private List<MetadataResponseDto> _metadataResponseDtos;


        private Mock<INavTechConfigurationService> _navTechConfigurationService;
        [TestInitialize]
        public void TestInitialize()
        {
            _navTechConfigurationService = new Mock<INavTechConfigurationService>();
            _controllerUnderTest = new ConfigurationController(_navTechConfigurationService.Object)
            {
                // Set Controller and Http Context
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            _metadataRequestModels = new List<MetadataRequestModel>
            {
                new MetadataRequestModel{ FieldName="FieldName1",IsRequired=true,MaxLength=10},
                new MetadataRequestModel{ FieldName="FieldName2",IsRequired=false,MaxLength=12},
                new MetadataRequestModel{ FieldName="FieldName3",IsRequired=true,MaxLength=23}
            };

            _configurationRequestModel = new ConfigurationRequestModel
            {
                EntityName = "DummyEntityName",
                Fields = _metadataRequestModels
            };

            _metadataResponseDtos = new List<MetadataResponseDto>
            {
                new MetadataResponseDto {FieldName= "FieldName1",IsRequired=true,MaxLength=10,Source="1"},
                new MetadataResponseDto {FieldName = "FieldName2",IsRequired=false,MaxLength=12,Source="2"},
                new MetadataResponseDto {FieldName ="FieldName3",IsRequired=true,MaxLength=11,Source="1"}
            };

            _configurationResponseDto = new ConfigurationResponseDto
            {
                EntityName = "DummyEntityName",
                Metadatas = _metadataResponseDtos
            };
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task AddConfiguration_Returns_Ok()
        {
            //Arrange
            _navTechConfigurationService.Setup(x => x.AddNewConfiguration(It.IsAny<ConfigurationRequestDto>())).ReturnsAsync(_configurationResponseDto).Verifiable();
            
            //Act
            var actionResult = await _controllerUnderTest.PostConfiguration(_configurationRequestModel);
            var objectResult = (ObjectResult)actionResult;
            var Response = (ConfigurationResponseModel)objectResult.Value;

            //Assert
            _navTechConfigurationService.VerifyAll();
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(objectResult);
            Assert.IsNotNull(Response);
            Assert.AreEqual(200, objectResult.StatusCode);
        }
        [TestMethod]
        [TestCategory("Unit")]
        public async Task GetConfiguration_Returns_Ok()
        {
            //Arrange
            _navTechConfigurationService.Setup(x => x.GetConfiguration(It.IsAny<string>())).ReturnsAsync(_configurationResponseDto).Verifiable();

            //Act
            var actionResult = await _controllerUnderTest.GetConfiguration("DummyEntityName");
            var objectResult = (ObjectResult)actionResult;
            var Response = (ConfigurationResponseModel)objectResult.Value;

            //Assert
            _navTechConfigurationService.VerifyAll();
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(objectResult);
            Assert.IsNotNull(Response);
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task GetConfiguration_Returns_BadRequest_When_Entity_Is_Null()
        {
            //Arrange
           
            //Act
            var actionResult = await _controllerUnderTest.GetConfiguration("");
            var objectResult = (BadRequestObjectResult)actionResult;

            //Assert
            _navTechConfigurationService.VerifyAll();
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult.StatusCode);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task GetConfiguration_Returns_InternalServerError_When_Service_throw_Exception()
        {
            //Arrange
            var exception = new ConfigEntityException("DummyException", System.Net.HttpStatusCode.InternalServerError);
            _navTechConfigurationService.Setup(x => x.GetConfiguration(It.IsAny<string>()))
                .ThrowsAsync(exception).Verifiable();
            //Act
            var actionResult = await _controllerUnderTest.GetConfiguration("DummyEntityName");
            var objectResult = (ObjectResult)actionResult;

            //Assert
            _navTechConfigurationService.VerifyAll();
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("DummyException", objectResult.Value.ToString());
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task DeleteConfiguration_Returns_Ok()
        {
            //Arrange
            _navTechConfigurationService.Setup(x => x.DeleteConfiguration(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            //Act
            var actionResult = await _controllerUnderTest.DeleteConfiguration("DummyEntityName");
            var objectResult = (NoContentResult)actionResult;

            //Assert
            _navTechConfigurationService.VerifyAll();
            _navTechConfigurationService.VerifyNoOtherCalls();
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(204, objectResult.StatusCode);
        }
        [TestMethod]
        [TestCategory("Unit")]
        public async Task DeleteMetadata_Returns_Ok()
        {
            //Arrange
            _navTechConfigurationService.Setup(x => x.DeleteEntityMetadata(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            //Act
            var actionResult = await _controllerUnderTest.DeleteMetadata("DummyEntityName", "Field1");
            var objectResult = (NoContentResult)actionResult;
            

            //Assert
            _navTechConfigurationService.VerifyAll();
            _navTechConfigurationService.VerifyNoOtherCalls();
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(204, objectResult.StatusCode);
        }
    }
}

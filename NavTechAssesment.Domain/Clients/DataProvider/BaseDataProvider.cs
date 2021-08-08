using NavTechAssesment.Domain.Clients.DataProvider.Field.Models.Response;
using NavTechAssesment.Domain.Clients.DataProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Clients.DataProvider
{
    public abstract class BaseDataProvider : IDataProvider
    {
        protected readonly IHttpClientFactory clientFactory;
        protected readonly HttpClient client;
        protected BaseDataProvider(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            client = clientFactory.CreateClient();
            client.BaseAddress = new Uri("");
            client.DefaultRequestHeaders.Add("x-Api-Key", "");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public abstract Task<IEnumerable<FieldsResponse>> RetrieveFieldDataAsync(string entity);

    }
}

using NavTechAssesment.Domain.Clients.DataProvider.Field.Models.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Clients.DataProvider.Field
{
    public class FieldDataProvider : BaseDataProvider
    {
        private static readonly string retieveCustomFieldsFormat = "api/customfields/{0}";
        private static readonly string retieveDefaultFieldsFormat = "api/defaultfields/{0}";
        
        public FieldDataProvider(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            
        }
        public override async Task<IEnumerable<FieldsResponse>> RetrieveFieldDataAsync(string entity)
        {
            var customFields = await GetCustomFields(entity).ConfigureAwait(false);
            var defaultFields = await GetDefaultFields(entity).ConfigureAwait(false);
            var result = customFields.Select(x => new FieldsResponse { Field = x, Source = "Source2" }).ToList();
            result.AddRange(defaultFields.Select(x => new FieldsResponse { Field = x, Source = "Source1" }));
            return result;
        }

        private async Task<IEnumerable<string>> GetCustomFields(string entity)
        {

            var customFieldResponse = await client.GetAsync(string.Format(retieveCustomFieldsFormat, entity)).ConfigureAwait(false);
            customFieldResponse.EnsureSuccessStatusCode();
            string responseBody = await customFieldResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JArray.Parse(responseBody).Values<string>();
             
        }
        private async Task<IEnumerable<string>> GetDefaultFields(string entity)
        {
            var customFieldResponse = await client.GetAsync(string.Format(retieveDefaultFieldsFormat, entity)).ConfigureAwait(false);
            customFieldResponse.EnsureSuccessStatusCode();
            string responseBody = await customFieldResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JArray.Parse(responseBody).Values<string>();
        }
    }
}

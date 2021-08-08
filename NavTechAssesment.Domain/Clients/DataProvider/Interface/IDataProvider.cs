using NavTechAssesment.Domain.Clients.DataProvider.Field.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.Domain.Clients.DataProvider.Interface
{
    public interface IDataProvider
    {
        Task<IEnumerable<FieldsResponse>> RetrieveFieldDataAsync(string entity);
    }
}

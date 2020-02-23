using CleanArchitecture.Domain.Interfaces;

using System.Net.Http;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class ExternalIdNumberValidationService : IIdNumberValidationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ExternalIdNumberValidationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> IsIdNumberValid(string idNumber)
        {
            var client = _clientFactory.CreateClient("idNumberValidation");

            var response = await client.GetAsync("/" + idNumber);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<bool>();
        }
    }
}

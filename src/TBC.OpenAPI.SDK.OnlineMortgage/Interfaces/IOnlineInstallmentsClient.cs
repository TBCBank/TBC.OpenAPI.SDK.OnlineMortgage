using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Responses;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Interfaces
{
    public interface IOnlineMortgageClient : IOpenApiClient
    {
        Task<InitiateMortgageLeadsResponse> InitiateOnlineMortgageLeads(InitiateMortgageLeadsRequest model, CancellationToken cancellationToken = default);
        Task<InitiateMortgageShortLeadsResponse> InitiateOnlineMortgageShortLeads(InitiateMortgageShortLeadsRequest model, CancellationToken cancellationToken = default);
    }
}

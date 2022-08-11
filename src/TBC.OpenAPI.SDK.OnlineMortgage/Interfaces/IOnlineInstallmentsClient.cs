

using System.Threading;
using System.Threading.Tasks;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage.Models;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Responses;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Interfaces
{
    public interface IOnlineMortgageClient : IOpenApiClient
    {
        Task<InitiateMortgageLeadsResponce> InitiateOnlineMortgageLeads(InitiateMortgageLeadsRequest model, CancellationToken cancellationToken = default);
        Task<InitiateMortgageShortLeadsResponce> InitiateOnlineMortgageShortLeads(InitiateMortgageShortLeadsRequest model, CancellationToken cancellationToken = default);

    }
}

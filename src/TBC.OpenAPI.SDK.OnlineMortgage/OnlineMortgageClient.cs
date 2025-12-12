using System.Net;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.Core.Exceptions;
using TBC.OpenAPI.SDK.Core.Models;
using TBC.OpenAPI.SDK.OnlineMortgage.Interfaces;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Responses;

namespace TBC.OpenAPI.SDK.OnlineMortgage
{
    public class OnlineMortgageClient : IOnlineMortgageClient
    {
        private readonly OnlineMortgageClientOptions _options;
        private readonly IHttpHelper<OnlineMortgageClient> _http;

        private static TokenResponse token { get; set; } = new TokenResponse();

        public OnlineMortgageClient(IHttpHelper<OnlineMortgageClient> http)
        {
            _http = http;
            UpdateToken(CancellationToken.None);
        }

        public async Task<InitiateMortgageLeadsResponse> InitiateOnlineMortgageLeads(InitiateMortgageLeadsRequest model, CancellationToken cancellationToken = default)
        {
            var result = await CallPost<InitiateMortgageLeadsRequest, InitiateMortgageLeadsResponse>(
                _http.PostJsonAsync<InitiateMortgageLeadsRequest, InitiateMortgageLeadsResponse>,
                "/v1/online-mortgages/leads",
                model,
                null,
                null,
                cancellationToken
                )
                .ConfigureAwait(false);

            if (!result.IsSuccess)
                throw new OpenApiException(result.Problem?.Title ?? "Unexpected error occurred", result.Exception);

            return result.Data;
        }

        public async Task<InitiateMortgageShortLeadsResponse> InitiateOnlineMortgageShortLeads(InitiateMortgageShortLeadsRequest model, CancellationToken cancellationToken = default)
        {
            var result = await CallPost<InitiateMortgageShortLeadsRequest, InitiateMortgageShortLeadsResponse>(
                _http.PostJsonAsync<InitiateMortgageShortLeadsRequest, InitiateMortgageShortLeadsResponse>,
                "/v1/online-mortgages/short-leads",
                model,
                null,
                null,
                cancellationToken
                )
                .ConfigureAwait(false);

            if (!result.IsSuccess)
                throw new OpenApiException(result.Problem?.Title ?? "Unexpected error occurred", result.Exception);

            return result.Data;
        }

        private async Task<ApiResponse<TResult>> CallPost<TData, TResult>(Func<string, TData, QueryParamCollection, HeaderParamCollection, CancellationToken, Task<ApiResponse<TResult>>> fn,
            string path, TData data, QueryParamCollection query, HeaderParamCollection headers, CancellationToken cancellationToken)
        {
            headers = headers ?? new HeaderParamCollection();
            headers.Add("Authorization", "Bearer " + token.Access_Token);

            ApiResponse<TResult> resp = await fn(path, data, query, headers, cancellationToken)
                .ConfigureAwait(false);

            if (resp?.Problem?.Status == (int)HttpStatusCode.Unauthorized)
            {
                UpdateToken(cancellationToken);
                headers["Authorization"] = "Bearer " + token.Access_Token;
                resp = await fn(path, data, query, headers, cancellationToken)
                    .ConfigureAwait(false);
            }

            return resp;
        }

        private async Task UpdateToken(CancellationToken cancellationToken)
        {
            var data = new UrlFormCollection 
            { 
                {"grant_type",TokenRequest.Grant_Type },
                {"scope",TokenRequest.Scope}
            };

            var response = Task.Run(() =>
           _http.PostUrlFormAsync<TokenResponse>("/oauth/token", data,  cancellationToken)
           ).Result;

            if (!response.IsSuccess)
                throw new OpenApiException(response.Problem?.Title ?? "Error Occurred while getting access token", response.Exception);

            token = response?.Data;
        }
    }
}

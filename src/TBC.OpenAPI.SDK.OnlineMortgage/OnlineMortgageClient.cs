using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.Core.Exceptions;
using TBC.OpenAPI.SDK.Core.Models;
using TBC.OpenAPI.SDK.OnlineMortgage.Interfaces;
using TBC.OpenAPI.SDK.OnlineMortgage.Models;
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


        public async Task<InitiateMortgageLeadsResponce> InitiateOnlineMortgageLeads(InitiateMortgageLeadsRequest model, CancellationToken cancellationToken = default)
        {
            var result = await CallPost<InitiateMortgageLeadsRequest, InitiateMortgageLeadsResponce>(
                _http.PostJsonAsync<InitiateMortgageLeadsRequest, InitiateMortgageLeadsResponce>,
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



        public async Task<InitiateMortgageShortLeadsResponce> InitiateOnlineMortgageShortLeads(InitiateMortgageShortLeadsRequest model, CancellationToken cancellationToken = default)
        {
            var result = await CallPost<InitiateMortgageShortLeadsRequest, InitiateMortgageShortLeadsResponce>(
                _http.PostJsonAsync<InitiateMortgageShortLeadsRequest, InitiateMortgageShortLeadsResponce>,
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





        private async Task<ApiResponse<TResult>> CallGet<TResult>(Func<string, QueryParamCollection, HeaderParamCollection, CancellationToken, Task<ApiResponse<TResult>>> fn,
            string path, QueryParamCollection query, HeaderParamCollection headers, CancellationToken cancellationToken)
        {
            headers = headers ?? new HeaderParamCollection();
            headers.Add("Authorization", "Bearer " + token.Access_Token);

            ApiResponse<TResult> resp = await fn(path, query, headers, cancellationToken)
                .ConfigureAwait(false);

            if (resp?.Problem?.Status == (int)HttpStatusCode.Unauthorized)
            {
                UpdateToken(cancellationToken);
                headers["Authorization"] = "Bearer " + token.Access_Token;
                resp = await fn(path, query, headers, cancellationToken)
                    .ConfigureAwait(false);
            }


            return resp;

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


            var responce = Task.Run(() =>
           _http.PostUrlFormAsync<TokenResponse>("/oauth/token", data,  cancellationToken)
           ).Result;

            if (!responce.IsSuccess)
                throw new OpenApiException(responce.Problem?.Title ?? "Error Occurred while getting access token", responce.Exception);


            token = responce?.Data;


        }
    }
}

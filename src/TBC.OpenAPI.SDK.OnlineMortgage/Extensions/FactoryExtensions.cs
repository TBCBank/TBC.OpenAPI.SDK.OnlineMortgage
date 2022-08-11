using System;
using System.Net.Http;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage.Interfaces;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Extensions
{
    public static class FactoryExtensions
    {
        public static OpenApiClientFactoryBuilder AddOnlineMortgageClient(this OpenApiClientFactoryBuilder builder,
            OnlineMortgageClientOptions options) => AddOnlineMortgageClient(builder, options, null, null);

        public static OpenApiClientFactoryBuilder AddOnlineMortgageClient(this OpenApiClientFactoryBuilder builder,
            OnlineMortgageClientOptions options,
            Action<HttpClient> configureClient = null,
            Func<HttpClientHandler> configureHttpMessageHandler = null)
        {
            return builder.AddClient<IOnlineMortgageClient, OnlineMortgageClient, OnlineMortgageClientOptions>(options, configureClient, configureHttpMessageHandler);
        }

        public static IOnlineMortgageClient GetOnlineMortgageClient(this OpenApiClientFactory factory) =>
            factory.GetOpenApiClient<IOnlineMortgageClient>();

    }
}
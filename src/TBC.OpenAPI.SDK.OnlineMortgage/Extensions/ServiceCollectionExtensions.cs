using Microsoft.Extensions.DependencyInjection;
using TBC.OpenAPI.SDK.Core.Extensions;
using TBC.OpenAPI.SDK.OnlineMortgage.Interfaces;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOnlineMortgageClient(this IServiceCollection services, OnlineMortgageClientOptions options) 
            => AddOnlineMortgageClient(services, options, null, null);

        public static IServiceCollection AddOnlineMortgageClient(this IServiceCollection services, OnlineMortgageClientOptions options,
            Action<HttpClient> configureClient = null,
            Func<HttpClientHandler> configureHttpMessageHandler = null)
        {
            services.AddOpenApiClient<IOnlineMortgageClient, OnlineMortgageClient, OnlineMortgageClientOptions>(options, configureClient, configureHttpMessageHandler);
            return services;
        }
    }
}

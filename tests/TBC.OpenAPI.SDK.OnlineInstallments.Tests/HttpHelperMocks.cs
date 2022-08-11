using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Responses;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Tests
{
    public class HttpHelperMocks
    {
        private readonly WireMockServer _mockServer;
        private readonly HttpClient _httpClient;

        public HttpClient HttpClient => _httpClient;
        public Mock<IOptions<OnlineMortgageClientOptions>> OptionsMock = new Mock<IOptions<OnlineMortgageClientOptions>>();

        public HttpHelperMocks()
        {
            _mockServer = WireMockServer.Start();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"{_mockServer.Urls[0]}/");



            AddMocks();
        }


        private void AddMocks()
        {
            #region OkResults

            OptionsMock.Setup(x => x.Value)
                .Returns(new OnlineMortgageClientOptions
                {
                    ApiKey = Guid.NewGuid().ToString(),
                    BaseUrl = "//BaseUrl",
                    ClientSecret = "ClientSecret"
                });

            _mockServer
                .Given(
                    Request.Create()
                    .WithPath("/v1/online-mortgages/leads")
                    .UsingMethod("POST")
                )
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBodyAsJson(new InitiateMortgageLeadsResponce
                        {
                            LeadId = Guid.NewGuid().ToString(),
                            RedirectUrl = "//SomeUrl"
                        })
                );



            _mockServer
                .Given(
                    Request.Create()
                    .WithPath("/v1/online-mortgages/short-leads")
                    .UsingMethod("POST")
                )
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBodyAsJson(new InitiateMortgageShortLeadsResponce
                        {
                            LeadId = Guid.NewGuid().ToString()
                        })
                );

            _mockServer
                .Given(
                    Request.Create()
                    .WithPath("/oauth/token")
                    .UsingMethod("POST")
                )
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBodyAsJson(new TokenResponse
                        {
                            Access_Token = Guid.NewGuid().ToString(),
                            Expires_In = DateTime.Now.ToString(),
                            Issued_At = DateTime.Now.ToString(),
                            Scope = "Test Scope",
                            Token_Type = "Test Type"
                        })
                );



            #endregion


        }
    }
}

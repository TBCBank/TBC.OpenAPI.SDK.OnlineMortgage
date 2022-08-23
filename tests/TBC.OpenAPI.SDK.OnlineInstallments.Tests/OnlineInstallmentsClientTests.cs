using FluentAssertions;
using FluentAssertions.Collections;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.Core.Exceptions;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Responses;
using Xunit;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Tests
{
    public class OnlineMortgageClientTests
    {
        private readonly OnlineMortgageClient _client;
        private readonly HttpHelperMocks _helperMocks;
        public OnlineMortgageClientTests()
        {
            _helperMocks = new HttpHelperMocks();
            var mock = new Mock<IHttpClientFactory>();
            mock.Setup(x => x.CreateClient(typeof(OnlineMortgageClient).FullName)).Returns(_helperMocks.HttpClient);
            var http = new HttpHelper<OnlineMortgageClient>(mock.Object);

            _client = new OnlineMortgageClient(http);
        }


        #region OkResults

        [Fact]
        public async Task InitiateOnlineMortgageLeads_WhenResponceOk_ReturnsData()
        {
            var result = await _client.InitiateOnlineMortgageLeads(new InitiateMortgageLeadsRequest
            {
                Url = "//SomeUrl",
                RealEstateCode = "FLAT",
                CompanyCode = "M2",
                OtherCompanyName = "",
                PropertyPrice = 196200.00m,
                PropertyPriceCurrencyCode = "GEL",
                DownPaymentAmount = 196200.00f,
                DownPaymentAmountCurrencyCode = "GEL",
                TermInMonths = 120
            });


            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.LeadId));
            Assert.True(!string.IsNullOrEmpty(result.RedirectUrl));
        }


        [Fact]
        public async Task InitiateOnlineMortgageShortLeads_WhenResponceOk_ReturnsData()
        {
            var result = await _client.InitiateOnlineMortgageShortLeads(new InitiateMortgageShortLeadsRequest
            {
                BirthDate = new DateTime(1991, 07, 22),
                Comment = "Some Comment",
                FirstName = "FirstName",
                LastName = "LastName",
                IsDeveloperOffer = true,
                MobileNumber = "599000000",
                MonthlyIncome = 1250200,
                PersonalNo = "12345678911",
                Url = "//SomeUrl",
                RealEstateCode = "FLAT",
                PropertyPrice = 196200.00m,
                PropertyPriceCurrencyCode = "GEL",
                TermInMonths = 120
            });


            Assert.NotNull(result);
            Assert.True(!string.IsNullOrEmpty(result.LeadId));
        }


        #endregion


    }
}

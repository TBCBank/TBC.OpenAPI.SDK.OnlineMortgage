// See https://aka.ms/new-console-template for more information



using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage;
using TBC.OpenAPI.SDK.OnlineMortgage.Extensions;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;

var factory = new OpenApiClientFactoryBuilder()
    .AddOnlineMortgageClient(new OnlineMortgageClientOptions
    {
        BaseUrl = "https://test-api.tbcbank.ge/",
        ApiKey = "{apikey}",
        ClientSecret = "{clientSecret}"
    })
    .Build();


var client = factory.GetOnlineMortgageClient();

var result = client.InitiateOnlineMortgageLeads(new InitiateMortgageLeadsRequest 
{
    Url = "http://my.ge/myhome/ka/pr/10872462/iyideba-mshenebare-bina",
    RealEstateCode = "FLAT",
    CompanyCode = "M2",
    OtherCompanyName = "",
    PropertyPrice = 196200.00m,
    PropertyPriceCurrencyCode = "GEL",
    DownPaymentAmount = 19620.00f,
    DownPaymentAmountCurrencyCode = "GEL",
    TermInMonths = 120
}).GetAwaiter().GetResult();

Console.WriteLine($"RedirectUrl: {result.RedirectUrl}, LeadId: { result.LeadId}");

Console.ReadLine();

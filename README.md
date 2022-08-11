# TBC.OpenAPI.SDK.OnlineMortgage
​
[![NuGet version (TBC.OpenAPI.SDK.OnlineMortgage)](https://img.shields.io/nuget/v/TBC.OpenAPI.SDK.OnlineMortgage.svg?label=TBC.OpenAPI.SDK.OnlineMortgage)](https://www.nuget.org/packages/TBC.OpenAPI.SDK.OnlineMortgage/) [![CI](https://github.com/TBCBank/TBC.OpenAPI.SDK.OnlineMortgage/actions/workflows/main.yml/badge.svg?branch=master)](https://github.com/TBCBank/TBC.OpenAPI.SDK.OnlineMortgage/actions/workflows/main.yml)\
​
Online Mortgage SDKs for TBC OpenAPI
​
## Online Mortgage SDK
​
Repository contains the SDK for simplifying TBC Open API Online Mortgage API invocations on C# client application side.\
​
Library is written in the C # programming language and is compatible with .netstandard2.0 and .net6.0.
​
## Prerequisites
​
In order to use the SDK it is mandatory to have **apikey** from TBC Bank's OpenAPI Devportal.\
​
[See more details how to get apikey](https://developers.tbcbank.ge/docs/get-apikey-and-secret)
​
## .Net Core Usage
​
First step is to configure appsettings.json file with Online Mortgage endpoint, TBC Portal **apikey** and ClientSecret\
​
appsettings.json
​
```json
​
"OnlineMortgage": {
​
"BaseUrl": "https://tbcbank-test.apigee.net/v1/online-mortgage/",
​
"ApiKey": "{apikey}",

"ClientSecret" : "{ClientSecret}"
​
}
​
```
​
Then add Online Mortgage client as an dependency injection\
​
Program.cs
​
```cs
​
builder.Services.AddOnlineMortgageClient(builder.Configuration.GetSection("OnlineMortgage").Get<OnlineMortgageClientOptions>());
​
```
​
After two steps above, setup is done and  Online Mortgage client can be injected in any container class:
​
Injection example:
​
```cs
​
private readonly IOnlineMortgageClient _OnlineMortgageClient;
​
public TestController(IOnlineMortgageClient OnlineMortgageClient)
​
{
​
_OnlineMortgageClient = OnlineMortgageClient;
​
}
​
```
​
Api invocation example:
​
```cs


var result = await _OnlineMortgageClient.InitiateOnlineMortgageLeads(new InitiateMortgageLeadsRequest 
{
    Url = "http://my.ge/myhome/ka/pr/10872462/iyideba-mshenebare-bina",
    RealEstateCode = "FLAT",
    CompanyCode = "M2",
    OtherCompanyName = "",
    PropertyPrice = "196200.00",
    PropertyPriceCurrencyCode = "GEL",
    DownPaymentAmount = 19620.00f,
    DownPaymentAmountCurrencyCode = "GEL",
    TermInMonths = 120
}).GetAwaiter().GetResult();
​
```
​
## NetFramework Usage
​
First step is to configure appsettings.json file with Online Mortgage endpoint, TBC Portal **apikey** and ClientSecret\
​
Web.config
​
```xml

​
<add key="OnlineMortgageUrl" value="https://tbcbank-test.apigee.net/v1/online-mortgage/" />
​
<add key="OnlineMortgageKey" value="{apikey}" />

<add key="OnlineMortgageClientSecret" value="{clientSecret}" />
​
```
​
In the Global.asax file at Application_Start() add following code\
​
Global.asax
​
```cs
​
new OpenApiClientFactoryBuilder()
​
.AddOnlineMortgageClient(new OnlineMortgageClientOptions
​
{
​
BaseUrl = ConfigurationManager.AppSettings["OnlineMortgageUrl"],
​
ApiKey = ConfigurationManager.AppSettings["OnlineMortgageKey"],

ApiKey = ConfigurationManager.AppSettings["OnlineMortgageClientSecret"]
​
})
​
.Build();
​
```
​
This code reads config parameters and then creates singleton OpenApiClientFactory, which is used to instantiate Online Mortgage client.\
​
OnlineMortgageClient class instantiation and invocation example:
​
```cs
​
var OnlineMortgageClient = OpenApiClientFactory.Instance.GetOnlineMortgageClient();
​
var result = await OnlineMortgageClient.GetApplicationStatus(new GetApplicationStatusRequest
            {
                MerchantKey = "aeb32470-4999-4f05-b271-b393325c8d8f",
                SessionId = "3293a41f-1ad0-4542-968a-a8480495b2d6"
            });
​
```
​
For more details see examples in repo:
​
​

[CoreApiAppExmaple](https://github.com/TBCBank/TBC.OpenAPI.SDK.OnlineMortgage/tree/master/examples/CoreApiAppExmaple)
​

[CoreConsoleAppExample](https://github.com/TBCBank/TBC.OpenAPI.SDK.OnlineMortgage/tree/master/examples/CoreConsoleAppExample)
​

[NetFrameworkExample](https://github.com/TBCBank/TBC.OpenAPI.SDK.OnlineMortgage/tree/master/examples/NetFrameworkExample)

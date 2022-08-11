using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage;
using TBC.OpenAPI.SDK.OnlineMortgage.Extensions;

namespace NetFrameworkExample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new OpenApiClientFactoryBuilder()
                .AddOnlineMortgageClient(new OnlineMortgageClientOptions
                {
                    
                    BaseUrl = ConfigurationManager.AppSettings["OnlineMortgageUrl"],
                    ApiKey = ConfigurationManager.AppSettings["OnlineMortgageKey"],
                    ClientSecret = ConfigurationManager.AppSettings["OnlineMortgageClientSecret"]
                })
                .Build();
        }
    }
}

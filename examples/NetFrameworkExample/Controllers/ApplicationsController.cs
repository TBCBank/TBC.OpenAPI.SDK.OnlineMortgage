using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage.Extensions;

namespace NetFrameworkExample.Controllers
{
    public class ApplicationsController : ApiController
    {

        public async Task<IHttpActionResult> Get()
        {
            var OnlineMortgageClient = OpenApiClientFactory.Instance.GetOnlineMortgageClient();

            var result = await OnlineMortgageClient.InitiateOnlineMortgageLeads(new InitiateMortgageLeadsRequest
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
            });

            return Ok(result);
        }
    }
}

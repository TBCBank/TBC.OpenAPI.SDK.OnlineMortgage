using System.Threading.Tasks;
using System.Web.Http;
using TBC.OpenAPI.SDK.Core;
using TBC.OpenAPI.SDK.OnlineMortgage.Extensions;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;

namespace NetFrameworkExample.Controllers
{
    public class ApplicationsController : ApiController
    {
        public async Task<IHttpActionResult> InitiateOnlineMortgageLeads()
        {
            var OnlineMortgageClient = OpenApiClientFactory.Instance.GetOnlineMortgageClient();

            var result = await OnlineMortgageClient.InitiateOnlineMortgageLeads(new InitiateMortgageLeadsRequest
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
            });

            return Ok(result);
        }
    }
}

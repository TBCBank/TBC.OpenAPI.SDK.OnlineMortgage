using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBC.OpenAPI.SDK.OnlineMortgage.Interfaces;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests;
using TBC.OpenAPI.SDK.OnlineMortgage.Models.Responses;

namespace CoreApiAppExmaple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IOnlineMortgageClient _OnlineMortgageClient;

        public TestController(IOnlineMortgageClient OnlineMortgageClient)
        {
            _OnlineMortgageClient = OnlineMortgageClient;
        }

        [HttpGet]
        public async Task<ActionResult<InitiateMortgageLeadsResponce>> GetApplicationStatus(InitiateMortgageLeadsRequest model, CancellationToken cancellationToken = default)
        {
            var result = await _OnlineMortgageClient.InitiateOnlineMortgageLeads(model, cancellationToken);

            return Ok(result);
        }
    }
}
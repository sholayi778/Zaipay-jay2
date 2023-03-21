 
using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using Zaipay.Service;

namespace FinmoAuFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinmoNzFlowController : ControllerBase
    {
        private IFinmoNzFlowService FinmoNzFlow;
        public FinmoNzFlowController(IFinmoNzFlowService finmoNzFlow)
        {
            FinmoNzFlow = finmoNzFlow;
        }

        [HttpPost]
        [Route("Create/Customer")]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerRequestNZObj createUser)
        {
            return Ok(await FinmoNzFlow.CreateCustomer(createUser));
        }

        [HttpPost]
        [Route("Create/Payin")]
        public async Task<ActionResult> CreatePayment([FromBody] PayInPoliRequestObj model)
        {
            return Ok(await FinmoNzFlow.CreatePayment(model));
        }
    }
}
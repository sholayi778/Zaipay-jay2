 
using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using Zaipay.Service;

namespace CanadaEftPayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanadaEftPaymentController : ControllerBase
    {
        private ICanadaEftPaymentService EftPaymentService;
        public CanadaEftPaymentController(ICanadaEftPaymentService eftPayment)
        {
            EftPaymentService = eftPayment;
        }

        [HttpPost]
        [Route("Create/Customer")]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerObj createUser)
        {
            return Ok(await EftPaymentService.CreateCustomer(createUser));
        }

        [HttpPost]
        [Route("Create/EFTTransaction")]
        public async Task<ActionResult> CreateEFTTransaction([FromBody] TransactionObj model)
        {
            return Ok(await EftPaymentService.CreateEFTTransaction(model));
        }

        [HttpPost]
        [Route("Search/EFTTransaction")]
        public async Task<ActionResult> SearchEFTTransaction([FromBody] SearchTransactionObj model)
        {
            return Ok(await EftPaymentService.SearchEFTTransaction(model));
        }

        [HttpPost]
        [Route("Cancel/EFTTransaction")]
        public async Task<ActionResult> CancelEFTTransaction([FromBody] CancelTransactionObj model)
        {
            return Ok(await EftPaymentService.CancelEFTTransaction(model));
        }

        [HttpPost]
        [Route("Update/EFTCustomerAccount")]
        public async Task<ActionResult> UpdateEFTCustomerAccount([FromBody] UpdateCustomerObj model)
        {
            return Ok(await EftPaymentService.UpdateEFTCustomerAccount(model));
        }
    }
}
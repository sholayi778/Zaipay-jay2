 
using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using Zaipay.Service;

namespace FinmoAuFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinmoAuFlowController : ControllerBase
    {
        private IFinmoAuFlow FinmoAuFlow;
        public FinmoAuFlowController(IFinmoAuFlow finmoAuFlow)
        {
            FinmoAuFlow = finmoAuFlow;
        }

        [HttpPost]
        [Route("Create/Customer")]
        public async Task<ActionResult> CreateCustomer([FromBody] FinmoCustomerObj createUser)
        {
            return Ok(await FinmoAuFlow.CreateCustomer(createUser));
        }

        [HttpPost]
        [Route("Create/Wallet")]
        public async Task<ActionResult> CreateWallet([FromBody] WalletRequestObj createWallet)
        {
            return Ok(await FinmoAuFlow.CreateWallet(createWallet));
        }

        [HttpPost]
        [Route("Create/Pay")]
        public async Task<ActionResult> CreatePay([FromBody] PayRequestObj createPay)
        {
            return Ok(await FinmoAuFlow.CreatePay(createPay));
        }

        [HttpPost]
        [Route("Create/Virtual/Account")]
        public async Task<ActionResult> CreateVirtualAccount([FromBody] VirtualAccountRequestObj model)
        {
            return Ok(await FinmoAuFlow.CreateVirtualAccount(model));
        }

        [HttpPost]
        [Route("Create/Pay/Poli")]
        public async Task<ActionResult> CreatePayinPoli([FromBody] PoliPayRequestObj model)
        {
            return Ok(await FinmoAuFlow.CreatePayinPoli(model));
        }

        [HttpGet]
        [Route("Get/Wallet/by/WalletId")]
        public ActionResult GetWalletById(string walletId)
        {

            return Ok(FinmoAuFlow.GetWalletById(walletId));
        }

        [HttpPost]
        [Route("Create/Wallet/Fund/Transfer")]
        public async Task<ActionResult> WalletFundTransfer([FromBody] WalletFundTransferRequest model)
        {
            return Ok(await FinmoAuFlow.WalletFundTransfer(model));
        }

        [HttpPost]
        [Route("Create/Virtual/Account/Simulate/Payin")]
        public async Task<ActionResult> VirtualAccountSimulate([FromBody] VirtualAccountRequest2 model)
        {
            return Ok(await FinmoAuFlow.VirtualAccountSimulate(model));
        }

        [HttpPost]
        [Route("Create/Payin/Simulate")]
        public async Task<ActionResult> SimulatePayIn([FromBody] SimulatePayIdRequest model)
        {
            return Ok(await FinmoAuFlow.SimulatePayIn(model));
        }
    }
}
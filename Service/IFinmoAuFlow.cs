
using System.Threading.Tasks;

namespace Zaipay.Service
{
    public interface IFinmoAuFlow
    {
        Task<CustomerResponseObject> CreateCustomer(FinmoCustomerObj request);
        Task<WalletResponseObj> CreateWallet(WalletRequestObj request);
        Task<PayResponseObj> CreatePay(PayRequestObj request);
        Task<VirtualAccountResponseObj> CreateVirtualAccount(VirtualAccountRequestObj request);
        Task<PoliPayResponseObj> CreatePayinPoli(PoliPayRequestObj request);
        Task<WalletResponseObj> GetWalletById(string wallet_id);
        Task<WalletFundTransferResponseObj> WalletFundTransfer(WalletFundTransferRequest request);
        Task<VirtualAccountResponseObj2> VirtualAccountSimulate(VirtualAccountRequest2 request);
        Task<SimulatePayIdResponseObj> SimulatePayIn(SimulatePayIdRequest request);
    }
}

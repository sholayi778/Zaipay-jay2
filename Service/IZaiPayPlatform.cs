using System.Collections.Generic;
using System.Threading.Tasks;
using Zaipay.Models;
using Zaipay.Models.WebHooks;
using Zaipay.ViewModels;

namespace Zaipay.Service
{
    public interface IZaiPayPlatform
    {
        Task<string> GenerateToken(TokenRequest request);
        Task<string> CreateZaiUser(CreateUserVm createUserVm);
        //Task<string> GetDigitalWalletAccountId(string id);
        //Task<string> CreateZaiPayout();
        //Task<string> CreateVirtualAccount(string walletId);

        Task<ZaiAccount> InitiateTransaction(InitateTransaction initateTransaction);
       //Task<ZaiAccount> GetZaiUser(string topRateId);

        //webhooks
        void VirtualAccountStatus(VirtualAccountObject virtualAccount);
        void IncomingFunds(Transactions transactions);

        //pre live only
        //Task<string> FundDigitalWallet(FundWalletNPP fundWalletNPP);
        List<TokenModel> GetToken();
    }
}

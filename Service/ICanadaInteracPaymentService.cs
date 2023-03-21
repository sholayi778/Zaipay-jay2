
using System.Threading.Tasks;

namespace Zaipay.Service
{
    public interface ICanadaInteracPaymentService
    {
        Task<RequestResponseObj> RequestInterac(RequestInteracObj request);
        Task<SearchInteracResponseObj> SearchRequestInterac(SearchRequestInteracObj request);
        Task<WalletResponseObj> GetPaymentLink(string rID);
    }
}


using System.Threading.Tasks;

namespace Zaipay.Service
{
    public interface ICanadaEftPaymentService
    {
        Task<CustomerEFTResponseObj> CreateCustomer(CreateCustomerObj request);
        Task<TransactionResponseObj> CreateEFTTransaction(TransactionObj request);
        Task<SearchResponseObj> SearchEFTTransaction(SearchTransactionObj request);
        Task<CustomerEFTResponseObj> CancelEFTTransaction(CancelTransactionObj request);
        Task<CustomerEFTResponseObj> UpdateEFTCustomerAccount(UpdateCustomerObj request);
    }
}

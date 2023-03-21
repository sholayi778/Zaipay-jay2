
using System.Threading.Tasks;

namespace Zaipay.Service
{
    public interface IFinmoNzFlowService
    {
        Task<CustomerResponseObj> CreateCustomer(CustomerRequestNZObj request);
        Task<PayinResponseObj> CreatePayment(PayInPoliRequestObj request);
    }
}
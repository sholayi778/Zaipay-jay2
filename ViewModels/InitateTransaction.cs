using System;

namespace Zaipay.ViewModels
{
    public class InitateTransaction
    {
        public Guid UserId { get; set; } 
        public string TopRateUserId { get; set; } 
        public string SourceCurrency { get; set; } 
        public string DestinationCurrency { get; set; } 
        public string ToCountry { get; set; } 
        public decimal Amount { get; set; }
        public Guid BeneficaryId { get; set; }

    }
}

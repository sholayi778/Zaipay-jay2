using System;
using System.ComponentModel.DataAnnotations;

namespace Zaipay.Models
{
    public class TransactionRecord
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string TransactionRefNo { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string SourceCurrencyCode { get; set; }

        [Required]
        public string ToCountry { get; set; }

        [Required]
        public string DestinationCurrency { get; set; }
        [Required]
        public string BeneficiaryId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string TopRateUserId { get; set; }

        [Required]
        public string TransactionStatus { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string AuthToken { get; set; }
    }
}
